using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AdminPanel.Services;
using AdminPanel.ViewModels.Order.ChangeOrderStatus.Query;
using AdminPanel.ViewModels.Order.CreateOrder.Query;
using AdminPanel.ViewModels.Order.GetOrderCard.Query;
using AdminPanel.Extensions;
using AdminPanel.QueryChecker;
using AdminPanel.WEB;

namespace AdminPanel.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	
	public class OrderController : ControllerBase
	{
		private OrderService orderService;
		public OrderController()
		{
			orderService = new OrderService();
		}
		

		/// <summary>
		/// Возвращает список заказов клиента
		/// </summary>
		[Route("get-client-orders")]
		[HttpGet]
		[Produces("application/json")]
		public async Task<IActionResult> GetClientOrders()
		{
			try
			{
				var clientId = Guid.Parse(HttpContext.User.Claims.First(item => item.Type == "id").Value);

				var result = await orderService.GetClientOrders(clientId);

				return new ObjectResultCreator().CreateObjectResult(result);
			}
			catch (Exception ex)
			{
				return new ObjectResult(new CustomInternalServerError("Что-то пошло не так в /get-client-orders", ex.Message));
			}
		}

		/// <summary>
		/// Возвращает список заказов продавца
		/// </summary>
		[Route("get-vendor-orders")]
		[HttpGet]
		[Produces("application/json")]
		public async Task<IActionResult> GetVendorOrders()
		{
			try
			{
				if (!HttpContext.User.Identity.IsAuthenticated)
					return new ObjectResult(new CustomUnauthorized("Доступ запрещен. Требуется авторизация."));
				else
					if(HttpContext.User.Claims.Single(item => item.Type == ClaimsIdentity.DefaultRoleClaimType).Value != "Продавец")
						return new ObjectResult(new CustomForbidden("Доступ запрещен. Требуется роль 'Продавец'."));

				var clientId = Guid.Parse(HttpContext.User.Claims.First(item => item.Type == "id").Value);

				var result = await orderService.GetVendorOrders(clientId);

				return new ObjectResultCreator().CreateObjectResult(result);
			}
			catch (Exception ex)
			{
				return new ObjectResult(new CustomInternalServerError("Что-то пошло не так в /get-vendor-orders", ex.Message));
			}
		}

		/// <summary>
		/// Возвращает каточку заказа
		/// </summary>
		[Route("get-order-card")]
		[HttpGet]
		[Produces("application/json")]
		public async Task<IActionResult> GetOrderCard([FromQuery] getOrderCard query)
		{
			try
			{
				if (!HttpContext.User.Identity.IsAuthenticated)
					return new ObjectResult(new CustomUnauthorized("Доступ запрещен. Требуется авторизация."));

				var validator = new OrderChecker().Check_GetOrderCard(query);
				if (validator != null)
					return new ObjectResult(validator);

				var clientId = Guid.Parse(HttpContext.User.Claims.First(item => item.Type == "id").Value);

				var result = await orderService.GetOrderCard(clientId, query.orderId);

				return new ObjectResultCreator().CreateObjectResult(result);
			}
			catch (Exception ex)
			{
				return new ObjectResult(new CustomInternalServerError("Что-то пошло не так в /get-order-card", ex.Message));
			}
		}

		/// <summary>
		/// Изменить статус заказа
		/// </summary>
		[Route("change-order-status")]
		[HttpPut]
		[Produces("application/json")]
		public async Task<IActionResult> ChangeOrderStatus([FromBody] changeOrderStatus query)
		{
			try
			{
				if (!HttpContext.User.Identity.IsAuthenticated)
					return new ObjectResult(new CustomUnauthorized("Доступ запрещен. Требуется авторизация."));

				var validator = new OrderChecker().Check_ChangeOrderStatus(query);
				if (validator != null)
					return new ObjectResult(validator);

				var clientId = Guid.Parse(HttpContext.User.Claims.First(item => item.Type == "id").Value);

				var result = await orderService.ChangeOrderStatus(clientId, query);

				return new ObjectResultCreator().CreateObjectResult(result);
			}
			catch (Exception ex)
			{
				return new ObjectResult(new CustomInternalServerError("Что-то пошло не так в /change-order-status", ex.Message));
			}
		}
	}
}
