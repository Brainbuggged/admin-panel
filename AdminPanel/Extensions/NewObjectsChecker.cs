using Microsoft.Extensions.Caching.Memory;
using AdminPanel.Services;
using AdminPanel.ViewModels.Order.ChangeOrderStatus.Query;
using AdminPanel.ViewModels.Product.CreateDraftProduct.Query;
using AdminPanel.ViewModels.Product.CreateProduct.Query;
using AdminPanel.ViewModels.Product.UpdateProduct.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminPanel.Core.Repositories.NSI_Product;
using AdminPanel.Core.Repositories.Par_Models;
using AdminPanel.Extensions;
using AdminPanel.Models;
using AdminPanel.ViewModels.Order.ChangeOrderStatus.Query;
using AdminPanel.ViewModels.Product.CreateDraftProduct.Query;
using AdminPanel.ViewModels.Product.CreateProduct.Query;
using AdminPanel.ViewModels.Product.UpdateProduct.Query;

namespace AdminPanel.Extensions
{
	public class NewObjectsChecker
	{
		public async Task<RequestResult> CheckNewProduct(createProduct createProduct)
		{
			if (!String.IsNullOrWhiteSpace(createProduct.product_type))
			{
				if (int.TryParse(createProduct.product_type, out int a) == false)
					return new RequestResult { status = ResultStatus.BadRequest, message = "Некорретный тип товара", result = null };
			}
			else
				createProduct.product_type = "0";

			if (String.IsNullOrWhiteSpace(createProduct.product_name))
				createProduct.product_name = "";

			if (String.IsNullOrWhiteSpace(createProduct.product_category))
				return new RequestResult { status = ResultStatus.BadRequest, message = "Некорретная категория товара", result = null };
			
			var par_category = await new CategoryRepository().GetByNameAsync(createProduct.product_category);
			var category = await new ProductCategoryRepository().GetByNameAsync(createProduct.product_category);
			if (category == null || par_category == null)
				return new RequestResult { status = ResultStatus.BadRequest, message = "Некорретная категория товара", result = null };
			else
			{
				var parameters = (await new ParameterRepository().GetByCategoryAsync(par_category.id));

				foreach (var item in createProduct.product_properties)
					if (parameters.FirstOrDefault(par => par == item.property_name) == null)
						return new RequestResult { status = ResultStatus.BadRequest, message = $"Недопустимый параметр товара: {item.property_name}", result = null };

				createProduct.product_category = category.id.ToString();
			}

			if ((ProductType)int.Parse(createProduct.product_type) == ProductType.BU && createProduct.product_amount != 1)
				return new RequestResult { status = ResultStatus.BadRequest, message = "Некорректное количество товара для типа Б/У", result = null };

			if (createProduct.product_release_year < DateTime.MinValue.Year|| createProduct.product_release_year > DateTime.MaxValue.Year)
				return new RequestResult { status = ResultStatus.BadRequest, message = "Некорректный год выпуска", result = null };

			return new RequestResult { status = ResultStatus.Ok, message = "", result = createProduct };
		}


		public async Task<RequestResult> CheckNewDraftProduct(createDraftProduct createProduct)
		{
			if (!String.IsNullOrWhiteSpace(createProduct.product_type))
			{
				if (int.TryParse(createProduct.product_type, out int a) == false)
					return new RequestResult { status = ResultStatus.BadRequest, message = "Некорретный тип товара", result = null };
			}
			else
				createProduct.product_type = "0";

			if (String.IsNullOrWhiteSpace(createProduct.product_name))
				createProduct.product_name = "";

			var par_category = await new CategoryRepository().GetByNameAsync(createProduct.product_category);
			var category = await new ProductCategoryRepository().GetByNameAsync(createProduct.product_category);
			if (category == null || par_category == null)
				return new RequestResult { status = ResultStatus.BadRequest, message = "Некорретная категория товара", result = null };
			else
			{
				var parameters = (await new ParameterRepository().GetByCategoryAsync(par_category.id));

				foreach (var item in createProduct.product_properties)
					if (parameters.FirstOrDefault(par => par == item.property_name) == null)
						return new RequestResult { status = ResultStatus.BadRequest, message = $"Некорретный параметр товара: {item}", result = null };

				createProduct.product_category = category.id.ToString();
			}

			if (String.IsNullOrWhiteSpace(createProduct.product_brand))
				createProduct.product_brand = "";

			if (String.IsNullOrWhiteSpace(createProduct.product_description))
				createProduct.product_description = "";

			if (createProduct.product_photoes == null)
				createProduct.product_photoes = new List<createProductPhoto>{ new createProductPhoto { is_main = true, linq = "" } };

			if (String.IsNullOrWhiteSpace(createProduct.product_address))
				createProduct.product_address = "";

			if (String.IsNullOrWhiteSpace(createProduct.product_country))
				createProduct.product_country = "";

			if (String.IsNullOrWhiteSpace(createProduct.product_city))
				createProduct.product_city = "";

			if ((ProductType)int.Parse(createProduct.product_type) == ProductType.BU && createProduct.product_amount != 1)
				return new RequestResult { status = ResultStatus.BadRequest, message = "Некорректное количество товара для типа Б/У", result = null };

			return new RequestResult { status = ResultStatus.Ok, message = "", result = createProduct };
		}
		public async Task<RequestResult> CheckUpdateProduct(updateProduct updateProduct)
		{
			if (!String.IsNullOrWhiteSpace(updateProduct.product_type))
			{
				if (int.TryParse(updateProduct.product_type, out int a) == false)
					return new RequestResult { status = ResultStatus.BadRequest, message = "Некорретный тип товара", result = null };
			}
			else
				updateProduct.product_type = "0";

			if (String.IsNullOrWhiteSpace(updateProduct.product_name))
				updateProduct.product_name = "";

			if (String.IsNullOrWhiteSpace(updateProduct.product_category))
				return new RequestResult { status = ResultStatus.BadRequest, message = "Некорретная категория товара", result = null };

			var par_category = await new CategoryRepository().GetByNameAsync(updateProduct.product_category);
			var category = await new ProductCategoryRepository().GetByNameAsync(updateProduct.product_category);
			if (category == null || par_category == null)
				return new RequestResult { status = ResultStatus.BadRequest, message = "Некорретная категория товара", result = null };
			else
			{
				var parameters = (await new ParameterRepository().GetByCategoryAsync(par_category.id));

				foreach (var item in updateProduct.product_properties)
					if (parameters.FirstOrDefault(par => par == item.property_name) == null)
						return new RequestResult { status = ResultStatus.BadRequest, message = $"Некорретный параметр товара: {item}", result = null };

				updateProduct.product_category = category.id.ToString();
			}

			if (String.IsNullOrWhiteSpace(updateProduct.product_brand))
				updateProduct.product_brand = "";

			if (String.IsNullOrWhiteSpace(updateProduct.product_description))
				updateProduct.product_description = "";

			if (updateProduct.product_photoes == null)
				updateProduct.product_photoes = new List<createProductPhoto> { new createProductPhoto { is_main = true, linq = "" } };

			if (String.IsNullOrWhiteSpace(updateProduct.product_address))
				updateProduct.product_address = "";

			if (String.IsNullOrWhiteSpace(updateProduct.product_country))
				updateProduct.product_country = "";

			if (String.IsNullOrWhiteSpace(updateProduct.product_city))
				updateProduct.product_city = "";

			if ((ProductType)int.Parse(updateProduct.product_type) == ProductType.BU && updateProduct.product_amount != 1)
				return new RequestResult { status = ResultStatus.BadRequest, message = "Некорректное количество товара для типа Б/У", result = null };

			return new RequestResult { status = ResultStatus.Ok, message = "", result = updateProduct };
		}
		public RequestResult CheckOrderStatus(changeOrderStatus changeOrderStatus)
		{
			OrderStatusForChange orderStatusForChange = new OrderStatusForChange();

			switch (changeOrderStatus.role)
			{
				case "Продавец":
					orderStatusForChange.roleType = RoleType.Seller;
					break;
				case "Покупатель":
					orderStatusForChange.roleType = RoleType.Client;
					break;
			}

			switch (changeOrderStatus.status)
			{
				case "В обработке":
					orderStatusForChange.newStatus = OrderStatus.vObrabotke;
					break;
				case "Ожидается отмена":
					orderStatusForChange.newStatus = OrderStatus.ojidaetsyaOtmena;
					break;
				case "Ожидается отправка":
					orderStatusForChange.newStatus = OrderStatus.ojidaetsyaOtpravka;
					break;
				case "В пути":
					orderStatusForChange.newStatus = OrderStatus.vPuti;
					break;
				case "Частичная отгрузка":
					orderStatusForChange.newStatus = OrderStatus.chastichnayaOtgruzka;
					break;
				case "Получено покупателем":
					orderStatusForChange.newStatus = OrderStatus.poluchenPokupatelem;
					break;
				case "Завершён":
					orderStatusForChange.newStatus = OrderStatus.zavershon;
					break;
				case "Отменён":
					orderStatusForChange.newStatus = OrderStatus.otmenen;
					break;
				case "Открытие спора":
					orderStatusForChange.newStatus = OrderStatus.otkritieSpora;
					break;
				case "Апелляция":
					orderStatusForChange.newStatus = OrderStatus.apellyatsiya;
					break;
				case "Возврат":
					orderStatusForChange.newStatus = OrderStatus.vozvrat;
					break;
				case "Отклонение возврата":
					orderStatusForChange.newStatus = OrderStatus.otkloneniyeVozvrata;
					break;
				case "Готов к отправке":
					orderStatusForChange.newStatus = OrderStatus.gotovKOtpravke;
					break;
				case "Заказ забран":
					orderStatusForChange.newStatus = OrderStatus.zakazZabran;
					break;
				case "Архив":
					orderStatusForChange.newStatus = OrderStatus.archive;
					break;
			}
			return new RequestResult { status = ResultStatus.Ok, message = "", result = orderStatusForChange };
		}
		public RequestResult CheckStatusSystem(OrderStatusForChange orderStatusForChange)
		{
			var checker = new StatusSystemExtension()._statusSystem
				.FirstOrDefault(item =>
					item.Delivery == orderStatusForChange.deliveryType &&
					item.Role == orderStatusForChange.roleType &&
					item.Status == orderStatusForChange.oldStatus &&
					item.NewStatuses.Contains(orderStatusForChange.newStatus) == true
				);
			if (checker == null)
				return new RequestResult { status = ResultStatus.BadRequest, message = "Невозможно присвоить указанный статус", result = null };

			return new RequestResult { status = ResultStatus.Ok, message = "", result = null };
		}
	}
}
