using AdminPanel.Extensions;
using AdminPanel.Repositories.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPanel.Core.Repositories.NSI_Client;
using AdminPanel.Core.Repositories.NSI_Order;
using AdminPanel.Core.Repositories.NSI_Product;
using AdminPanel.Models;
using AdminPanel.Models.Models.NSI_Order;
using AdminPanel.ViewModels.Order.ChangeOrderStatus.Query;
using AdminPanel.ViewModels.Order.GetClientOrders.Response;
using AdminPanel.ViewModels.Order.GetOrderCard.Response;
using AdminPanel.Extensions;

namespace AdminPanel.Services
{
	public class OrderService
	{
		///////////////////////////////////////////////////////////////////////////////////////////
		public async Task<RequestResult> GetClientOrders(Guid clientId)
		{
			var client = await new ClientRepository().GetByIDAsync(clientId);
			if (client == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId} не найден", result = null };

			var orders = await new OrderRepository().GetByClientIdAsync(clientId);

			var groupedOrders = orders.GroupBy(order => order.number);
			var clientOrders = new List<ResponseClientOrder>();
			foreach (var group in groupedOrders)
			{
				var clientOrder = new ResponseClientOrder
				{
					order_date = group.First().date,
					order_number = group.First().number,
					order_position_count = group.Sum(gr => gr.position_count),
					sub_orders = new List<ResponseClientSubOrder>(),
					order_total_prise = group.Sum(gr => gr.total_prise),
					order_id = group.First().id
				};

				foreach (var ord in group)
				{
					clientOrder.sub_orders.Add(new ResponseClientSubOrder
					{
						order_status = ord.status.GetText(),
						position_count = ord.position_count,
						vendor_name = ord.vendor_name
					});
				}

				clientOrders.Add(clientOrder);
			}

			return new RequestResult { status = ResultStatus.Ok, message = "", result = clientOrders.OrderByDescending(ord => ord.order_number).ToList() };
		}
		///////////////////////////////////////////////////////////////////////////////////////////
		public async Task<RequestResult> GetVendorOrders(Guid clientId)
		{
			var client = await new ClientRepository().GetByIDAsync(clientId);
			if (client == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId} не найден", result = null };
			if (client.role != RoleType.Seller)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId}  не является продавцом", result = null };

			var orders = (await new OrderRepository().GetByVendorIdAsync((Guid)client.vendorid)).ToList();
			orders.ForEach(item =>
			{
				item.order_status = ((OrderStatus)int.Parse(item.order_status)).GetText();
			});

			return new RequestResult { status = ResultStatus.Ok, message = "", result = orders.OrderByDescending(ord => ord.order_number).ToList() };
		}
		///////////////////////////////////////////////////////////////////////////////////////////
		public async Task<RequestResult> GetOrderCard(Guid clientId, Guid orderId)
		{
			var client = await new ClientRepository().GetByIDAsync(clientId);
			if (client == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId} не найден", result = null };

			var order = new ResponseOrderCard();
			if (client.role == RoleType.Seller)
				order = await new OrderRepository().GetOrderCard(clientId, (Guid)client.vendorid, orderId);
			// else
			// 	order = await new OrderRepository().GetOrderCard(clientId, orderId);

			order.order_status = ((OrderStatus)int.Parse(order.order_status)).GetText();

			order.order_products = (await new OrderProductRepository().GetByOrderNumberAsync(orderId)).OrderBy(item => item.product_name).ToList();
			order.order_statuses = (await new OrderStatusChangeRepository().GetByOrderNumberAsync(orderId)).OrderBy(item => item.change_date).ToList();
			order.order_statuses.ForEach(item =>
			{
				item.order_old_status = ((OrderStatus)int.Parse(item.order_old_status)).GetText();
				item.order_new_status = ((OrderStatus)int.Parse(item.order_new_status)).GetText();
			});

			order.order_delivery_type = ((DeliveryType)int.Parse(order.order_delivery_type)).GetText();

			return new RequestResult { status = ResultStatus.Ok, message = "", result = order };
		}
		///////////////////////////////////////////////////////////////////////////////////////////
		public async Task<RequestResult> ChangeOrderStatus(Guid clientId, changeOrderStatus query)
		{
			var client = await new ClientRepository().GetByIDAsync(clientId);
			if (client == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId} не найден", result = null };

			var checkedOrderStatus = new NewObjectsChecker().CheckOrderStatus(query);
			var newOrderStatus = (OrderStatusForChange)checkedOrderStatus.result;

			var order = newOrderStatus.roleType == RoleType.Seller ?
				await new OrderRepository().GetByNumberAndVendorAsync(query.orderId, (Guid)client.vendorid) :
				await new OrderRepository().GetByNumberAndClientAsync(query.orderId, client.id);
			if (order == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Заказ с номером {query.orderId} не существует", result = null };

			newOrderStatus.oldStatus = order.status;
			newOrderStatus.deliveryType = order.delivery_type;

			checkedOrderStatus = new NewObjectsChecker().CheckStatusSystem(newOrderStatus);
			if (checkedOrderStatus.status == ResultStatus.BadRequest)
				return new RequestResult { status = ResultStatus.BadRequest, message = checkedOrderStatus.message, result = null };

			await new OrderRepository().UpdateStatus(order.id, newOrderStatus.newStatus);

			var vendor = await new HangfireRepository().GetVendorAsync(order.vendorid);


			var orderStatus = new OrderStatusChangeModel
			{
				id = Guid.NewGuid(),
				date = new SettingsExtension().GetDateTimeNow(),
				old_status = newOrderStatus.oldStatus,
				new_status = newOrderStatus.newStatus,
				time = new SettingsExtension().GetDateTimeNow(),
				orderid = order.id
			};
			await new OrderStatusChangeRepository().AddAsync(orderStatus);


			return new RequestResult { status = ResultStatus.Accepted, message = "Статус заказа успешно изменен", result = null };
		}
	}
}
