using AdminPanel.Models.Catalog.GetCatalogProducts.Query;
using AdminPanel.Models.Catalog.GetCategoryParameters.Query;
using AdminPanel.Models.Catalog.GetProductCard.Query;
using AdminPanel.Models.Catalog.GetVendorCard.Query;
using AdminPanel.ViewModels.Catalog.GetChat.Query;
using AdminPanel.ViewModels.Catalog.SendChatMessage.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminPanel.Extensions;

namespace AdminPanel.QueryChecker
{
	public class CatalogChecker
	{
		public CustomBadRequest Check_GetCategoryParameters(getCategoryParameters query)
		{
			List<string> errors = new List<string>();

			if (String.IsNullOrWhiteSpace(query.categoryName))
				errors.Add($"Параметр categoryName явяляется обязательным для заполнения");

			if (errors.Count != 0)
				return new CustomBadRequest("При валидации данных произошла ошибка", errors);
			else
				return null;
		}
		public CustomBadRequest Check_GetCatalogProducts(string productCategory, int productCount,int pageNumber,int sortingType,bool bestRatingFirst,bool productInStock,int productType, List<GetProductsWithFilters> filters)
		{
			List<string> errors = new List<string>();

			if (String.IsNullOrWhiteSpace(productCategory))
				errors.Add($"Параметр productCategory явяляется обязательным для заполнения");

			if (int.TryParse(productCount.ToString(), out int a) == false)
				errors.Add($"Тип данных для productCount должен быть int");
			else
				if(int.Parse(productCount.ToString()) % 12 != 0 || int.Parse(productCount.ToString()) < 12)
				errors.Add($"Значение productCount должно быть больше 0 и кратно 12");

			if (int.TryParse(pageNumber.ToString(), out a) == false)
				errors.Add($"Тип данных для pageNumber должен быть int");
			else
				if (int.Parse(pageNumber.ToString()) < 1)
				errors.Add($"Значение pageNumber должно быть больше 0");

			if (int.TryParse(sortingType.ToString(), out a) == false)
				errors.Add($"Тип данных для sortingType должен быть int");
			else
				if (int.Parse(sortingType.ToString()) > 1)
				errors.Add($"Значение sortingType должно быть больше 0");

			if (int.TryParse(productType.ToString(), out a) == false)
				errors.Add($"Тип данных для productType должен быть int");
			else
				if (int.Parse(productType.ToString()) > 1)
				errors.Add($"Значение productType должно быть больше 0");

			if (bool.TryParse(productInStock.ToString(), out bool b) == false)
				errors.Add($"Тип данных для productInStock должен быть bool");

			if (bool.TryParse(bestRatingFirst.ToString(), out b) == false)
				errors.Add($"Тип данных для bestRatingFirst должен быть bool");

			if (filters == null)
				errors.Add($"Параметр filters явяляется обязательным для заполнения");

			if (errors.Count != 0)
				return new CustomBadRequest("При валидации данных произошла ошибка", errors);
			else
				return null;
		}

		public CustomBadRequest Check_GetProductCard(getProductCard query)
		{
			List<string> errors = new List<string>();

			if (String.IsNullOrWhiteSpace(query.productNumber))
				errors.Add($"Параметр productNumber явяляется обязательным для заполнения");

			if (errors.Count != 0)
				return new CustomBadRequest("При валидации данных произошла ошибка", errors);
			else
				return null;
		}

		public CustomBadRequest Check_GetVendorCard(getVendorCard query)
		{
			List<string> errors = new List<string>();

			if (String.IsNullOrWhiteSpace(query.vendorNumber))
				errors.Add($"Параметр vendorNumber явяляется обязательным для заполнения");

			if (errors.Count != 0)
				return new CustomBadRequest("При валидации данных произошла ошибка", errors);
			else
				return null;
		}

		public CustomBadRequest Check_SendChatMessage(sendChatMessage query)
		{
			List<string> errors = new List<string>();

			if (string.IsNullOrWhiteSpace(query.reciever_number))
				errors.Add($"Параметр reciever_number явяляется обязательным для заполнения");

			if (string.IsNullOrWhiteSpace(query.message_text))
				errors.Add($"Параметр message_text явяляется обязательным для заполнения");
			else
				if (query.message_text.Length > 1000)
					errors.Add($"Длина message_text не может быть больше 1000 символов");

			if (string.IsNullOrWhiteSpace(query.sender_role))
				errors.Add($"Параметр sender_role явяляется обязательным для заполнения");
			else
				if (query.sender_role != "Продавец" && query.sender_role != "Покупатель")
					errors.Add($"Значение sender_role может быть только 'Продавец' или 'Покупатель'");

			if (errors.Count != 0)
				return new CustomBadRequest("При валидации данных произошла ошибка", errors);
			else
				return null;
		}

		public CustomBadRequest Check_GetChat(getChat query)
		{
			List<string> errors = new List<string>();

			if (string.IsNullOrWhiteSpace(query.parthnerNumber))
				errors.Add($"Параметр parthnerNumber явяляется обязательным для заполнения");

			if (string.IsNullOrWhiteSpace(query.role))
				errors.Add($"Параметр role явяляется обязательным для заполнения");
				if (query.role != "Продавец" && query.role != "Покупатель")
					errors.Add($"Значение role может быть только 'Продавец' или 'Покупатель'");

			if (errors.Count != 0)
				return new CustomBadRequest("При валидации данных произошла ошибка", errors);
			else
				return null;
		}
	}
}
