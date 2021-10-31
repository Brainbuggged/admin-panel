using AdminPanel.ViewModels.Product.ActivateProduct.Query;
using AdminPanel.ViewModels.Product.ChangeMyProductCount.Query;
using AdminPanel.ViewModels.Product.ChangeMyProductDelivery.Query;
using AdminPanel.ViewModels.Product.CloseDraftRequest.Query;
using AdminPanel.ViewModels.Product.CreateDraftProduct.Query;
using AdminPanel.ViewModels.Product.CreateDraftRequest;
using AdminPanel.ViewModels.Product.CreateProduct.Query;
using AdminPanel.ViewModels.Product.DeactivateProduct.Query;
using AdminPanel.ViewModels.Product.GetDraftProduct.Query;
using AdminPanel.ViewModels.Product.GetProductParameters;
using AdminPanel.ViewModels.Product.RemoveDraftProduct.Query;
using AdminPanel.ViewModels.Product.UpdateProduct.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminPanel.Extensions;

namespace AdminPanel.QueryChecker
{
	public class ProductChecker
	{
		public CustomBadRequest Check_CreateProduct(createProduct query)
		{
			List<string> errors = new List<string>();

			if (string.IsNullOrWhiteSpace(query.product_type) == true)
				errors.Add($"Параметр product_type явяляется обязательным для заполнения");
			else
				if (int.TryParse(query.product_type.ToString(), out int tt) == false)
					errors.Add($"Тип данных для product_type должен быть int");
				else
					if (int.Parse(query.product_type.ToString()) < 0 || int.Parse(query.product_type.ToString()) > 1)
					errors.Add($"Значение product_type должно быть от 0 до 1");
			//////////////////////////////////////////////////////////////////////////
			if (String.IsNullOrWhiteSpace(query.product_name))
				errors.Add($"Параметр product_name явяляется обязательным для заполнения");
			//////////////////////////////////////////////////////////////////////////
			if (String.IsNullOrWhiteSpace(query.product_category))
				errors.Add($"Параметр product_category явяляется обязательным для заполнения");
			//////////////////////////////////////////////////////////////////////////
			if (query.product_properties.Count() < 1)
				errors.Add($"Количество параметров для product_properties должно быть минимум 1");
			//////////////////////////////////////////////////////////////////////////
			if (query.product_photoes.Count() < 1 || query.product_photoes.Count() > 10)
				errors.Add($"Количество фотография для product_photoes должно быть от 1 до 10");
			else
				if (query.product_photoes.FirstOrDefault(item => item.is_main == true) == null)
					errors.Add($"Не указана основная фотограия product_photoes");
			//////////////////////////////////////////////////////////////////////////
			if (String.IsNullOrWhiteSpace(query.product_brand))
				errors.Add($"Параметр product_brand явяляется обязательным для заполнения");
			//////////////////////////////////////////////////////////////////////////
			if (String.IsNullOrWhiteSpace(query.product_description))
				errors.Add($"Параметр product_description явяляется обязательным для заполнения");
			else
				if (query.product_description.Length > 800)
					errors.Add($"Длина product_description должен быть не более 800");
			//////////////////////////////////////////////////////////////////////////
			if (String.IsNullOrWhiteSpace(query.product_address))
				errors.Add($"Параметр product_address явяляется обязательным для заполнения");
			//////////////////////////////////////////////////////////////////////////
			if (String.IsNullOrWhiteSpace(query.product_country))
				errors.Add($"Параметр product_country явяляется обязательным для заполнения");
			//////////////////////////////////////////////////////////////////////////
			if (String.IsNullOrWhiteSpace(query.product_city))
				errors.Add($"Параметр product_city явяляется обязательным для заполнения");
			//////////////////////////////////////////////////////////////////////////
			if (double.TryParse(query.product_prise.ToString(), out double b) == false)
				errors.Add($"Тип данных для product_prise должен быть double");
			else
				if (double.Parse(query.product_prise.ToString()) < 0)
				errors.Add($"Значение product_prise должно быть больше 0");
			//////////////////////////////////////////////////////////////////////////
			if (int.TryParse(query.product_release_year.ToString(), out int a) == false)
				errors.Add($"Тип данных для product_release_year должен быть int");
			else
				if (int.Parse(query.product_release_year.ToString()) < DateTime.MinValue.Year || int.Parse(query.product_release_year.ToString()) > DateTime.MaxValue.Year)
				errors.Add($"Значение product_release_year должно быть от {DateTime.MinValue.Year} до {DateTime.MaxValue.Year}");
			//////////////////////////////////////////////////////////////////////////
			if (int.TryParse(query.product_amount.ToString(), out a) == false)
				errors.Add($"Тип данных для product_amount должен быть int");
			else
				if (int.Parse(query.product_amount.ToString()) < 1)
				errors.Add($"Значение product_amount должно быть больше 0");
			//////////////////////////////////////////////////////////////////////////
			if (bool.TryParse(query.product_is_pickuped.ToString(), out bool c) == false)
				errors.Add($"Тип данных для product_is_pickuped должен быть bool");
			//////////////////////////////////////////////////////////////////////////
			if (bool.TryParse(query.product_is_delivered.ToString(), out c) == false)
				errors.Add($"Тип данных для product_is_delivered должен быть bool");
			//////////////////////////////////////////////////////////////////////////
			if (double.TryParse(query.product_x.ToString(), out b) == false)
				errors.Add($"Тип данных для product_x должен быть double");
			else
				if (double.Parse(query.product_x.ToString()) < 0)
				errors.Add($"Значение product_x должно быть больше 0");
			//////////////////////////////////////////////////////////////////////////
			if (double.TryParse(query.product_y.ToString(), out b) == false)
				errors.Add($"Тип данных для product_y должен быть double");
			else
				if (double.Parse(query.product_y.ToString()) < 0)
				errors.Add($"Значение product_y должно быть больше 0");

			if (errors.Count != 0)
				return new CustomBadRequest("При валидации данных произошла ошибка", errors);
			else
				return null;
		}

		public CustomBadRequest Check_GetProductParameters(getProductParameters query)
		{
			List<string> errors = new List<string>();

			if (String.IsNullOrWhiteSpace(query.categoryName))
				errors.Add($"Параметр categoryName явяляется обязательным для заполнения");

			if (errors.Count != 0)
				return new CustomBadRequest("При валидации данных произошла ошибка", errors);
			else
				return null;
		}

		public CustomBadRequest Check_CreateDraftProduct(createDraftProduct query)
		{
			List<string> errors = new List<string>();

			if (String.IsNullOrWhiteSpace(query.product_name))
				errors.Add($"Параметр product_name явяляется обязательным для заполнения");
			//////////////////////////////////////////////////////////////////////////
			if (String.IsNullOrWhiteSpace(query.product_category))
				errors.Add($"Параметр product_category явяляется обязательным для заполнения");
			//////////////////////////////////////////////////////////////////////////
			if (query.product_properties.Count() < 1)
				errors.Add($"Параметр product_properties явяляется обязательным для заполнения");

			if (errors.Count != 0)
				return new CustomBadRequest("При валидации данных произошла ошибка", errors);
			else
				return null;
		}

		public CustomBadRequest Check_OpenDraftRequest(openDraftRequest query)
		{
			List<string> errors = new List<string>();

			if (String.IsNullOrWhiteSpace(query.category))
				errors.Add($"Параметр category явяляется обязательным для заполнения");
			//////////////////////////////////////////////////////////////////////////
			if (String.IsNullOrWhiteSpace(query.message))
				errors.Add($"Параметр message явяляется обязательным для заполнения");

			if (errors.Count != 0)
				return new CustomBadRequest("При валидации данных произошла ошибка", errors);
			else
				return null;
		}

		public CustomBadRequest Check_CloseDraftRequest(closeDraftRequest query)
		{
			List<string> errors = new List<string>();

			if (int.TryParse(query.requestNumber.ToString(), out int a) == false)
				errors.Add($"Тип данных для requestNumber должен быть int");
			else
				if (int.Parse(query.requestNumber.ToString()) < 1)
				errors.Add($"Значение requestNumber должно быть больше 0");

			if (errors.Count != 0)
				return new CustomBadRequest("При валидации данных произошла ошибка", errors);
			else
				return null;
		}

		public CustomBadRequest Check_GetProductToUpdate(getProductToUpdate query)
		{
			List<string> errors = new List<string>();

			if (Guid.TryParse(query.productId.ToString(), out Guid a) == false)
				errors.Add($"Тип данных для productId должен быть uuid");

			if (errors.Count != 0)
				return new CustomBadRequest("При валидации данных произошла ошибка", errors);
			else
				return null;
		}

		public CustomBadRequest Check_ArchiveProduct(archiveProduct query)
		{
			List<string> errors = new List<string>();

			if (Guid.TryParse(query.productId.ToString(), out Guid a) == false)
				errors.Add($"Тип данных для productId должен быть uuid");

			if (errors.Count != 0)
				return new CustomBadRequest("При валидации данных произошла ошибка", errors);
			else
				return null;
		}

		public CustomBadRequest Check_ActivateProduct(activateProduct query)
		{
			List<string> errors = new List<string>();

			if (Guid.TryParse(query.productId.ToString(), out Guid a) == false)
				errors.Add($"Тип данных для productId должен быть uuid");

			if (errors.Count != 0)
				return new CustomBadRequest("При валидации данных произошла ошибка", errors);
			else
				return null;
		}

		public CustomBadRequest Check_DeactivateProduct(deActivateProduct query)
		{
			List<string> errors = new List<string>();

			if (Guid.TryParse(query.productId.ToString(), out Guid a) == false)
				errors.Add($"Тип данных для productId должен быть uuid");

			if (errors.Count != 0)
				return new CustomBadRequest("При валидации данных произошла ошибка", errors);
			else
				return null;
		}

		public CustomBadRequest Check_ChangeMyProductCount(changeMyProductCount query)
		{
			List<string> errors = new List<string>();

			if (Guid.TryParse(query.productId.ToString(), out Guid a) == false)
				errors.Add($"Тип данных для productId должен быть uuid");

			if (int.TryParse(query.productCount.ToString(), out int b) == false)
				errors.Add($"Тип данных для productCount должен быть int");
			else
				if (int.Parse(query.productCount.ToString()) < 0)
				errors.Add($"Значение productCount должно быть больше 0");

			if (errors.Count != 0)
				return new CustomBadRequest("При валидации данных произошла ошибка", errors);
			else
				return null;
		}

		public CustomBadRequest Check_ChangeMyProductDelivery(changeMyProductDelivery query)
		{
			List<string> errors = new List<string>();

			if (Guid.TryParse(query.productId.ToString(), out Guid a) == false)
				errors.Add($"Тип данных для productId должен быть uuid");

			if (errors.Count != 0)
				return new CustomBadRequest("При валидации данных произошла ошибка", errors);
			else
				return null;
		}

		public CustomBadRequest Check_UpdateProduct(updateProduct query)
		{
			List<string> errors = new List<string>();

			if (Guid.TryParse(query.product_id.ToString(), out Guid a) == false)
				errors.Add($"Тип данных для product_id должен быть uuid");

			if (int.TryParse(query.product_type.ToString(), out int b) == false)
				errors.Add($"Тип данных для product_type должен быть int");
			else
				if (int.Parse(query.product_type.ToString()) < 0 || int.Parse(query.product_type.ToString()) > 1)
				errors.Add($"Значение product_type должно быть от 0 до 1");
			//////////////////////////////////////////////////////////////////////////
			if (String.IsNullOrWhiteSpace(query.product_name))
				errors.Add($"Параметр product_name явяляется обязательным для заполнения");
			//////////////////////////////////////////////////////////////////////////
			if (String.IsNullOrWhiteSpace(query.product_category))
				errors.Add($"Параметр product_category явяляется обязательным для заполнения");
			//////////////////////////////////////////////////////////////////////////
			if (query.product_properties.Count() < 1)
				errors.Add($"Параметр product_properties явяляется обязательным для заполнения");
			//////////////////////////////////////////////////////////////////////////
			if (query.product_photoes.Count() < 1)
				errors.Add($"Параметр product_photoes явяляется обязательным для заполнения");
			//////////////////////////////////////////////////////////////////////////
			if (String.IsNullOrWhiteSpace(query.product_brand))
				errors.Add($"Параметр product_brand явяляется обязательным для заполнения");
			//////////////////////////////////////////////////////////////////////////
			if (String.IsNullOrWhiteSpace(query.product_description))
				errors.Add($"Параметр product_description явяляется обязательным для заполнения");
			else
				if (query.product_description.Length > 800)
				errors.Add($"Длина product_description должен быть не более 800");
			//////////////////////////////////////////////////////////////////////////
			if (String.IsNullOrWhiteSpace(query.product_address))
				errors.Add($"Параметр product_address явяляется обязательным для заполнения");
			//////////////////////////////////////////////////////////////////////////
			if (String.IsNullOrWhiteSpace(query.product_country))
				errors.Add($"Параметр product_country явяляется обязательным для заполнения");
			//////////////////////////////////////////////////////////////////////////
			if (String.IsNullOrWhiteSpace(query.product_city))
				errors.Add($"Параметр product_city явяляется обязательным для заполнения");
			//////////////////////////////////////////////////////////////////////////
			if (double.TryParse(query.product_prise.ToString(), out double c) == false)
				errors.Add($"Тип данных для product_prise должен быть double");
			else
				if (double.Parse(query.product_prise.ToString()) < 0)
				errors.Add($"Значение product_prise должно быть больше 0");
			//////////////////////////////////////////////////////////////////////////
			if (int.TryParse(query.product_release_year.ToString(), out b) == false)
				errors.Add($"Тип данных для product_release_year должен быть int");
			else
				if (int.Parse(query.product_release_year.ToString()) < DateTime.MinValue.Year || int.Parse(query.product_release_year.ToString()) > DateTime.MaxValue.Year)
				errors.Add($"Значение product_release_year должно быть от {DateTime.MinValue.Year} до {DateTime.MaxValue.Year}");
			//////////////////////////////////////////////////////////////////////////
			if (query.product_photoes.Count() < 1 || query.product_photoes.Count() > 10)
				errors.Add($"Количество фотография для product_photoes должно быть от 1 до 10");
			//////////////////////////////////////////////////////////////////////////
			if (int.TryParse(query.product_amount.ToString(), out b) == false)
				errors.Add($"Тип данных для product_amount должен быть int");
			else
				if (int.Parse(query.product_amount.ToString()) < 1)
				errors.Add($"Значение product_amount должно быть больше 0");
			//////////////////////////////////////////////////////////////////////////
			if (bool.TryParse(query.product_is_pickuped.ToString(), out bool d) == false)
				errors.Add($"Тип данных для product_is_pickuped должен быть bool");
			//////////////////////////////////////////////////////////////////////////
			if (bool.TryParse(query.product_is_delivered.ToString(), out d) == false)
				errors.Add($"Тип данных для product_is_delivered должен быть bool");
			//////////////////////////////////////////////////////////////////////////
			if (double.TryParse(query.product_x.ToString(), out c) == false)
				errors.Add($"Тип данных для product_x должен быть double");
			else
				if (double.Parse(query.product_x.ToString()) < 0)
				errors.Add($"Значение product_x должно быть больше 0");
			//////////////////////////////////////////////////////////////////////////
			if (double.TryParse(query.product_y.ToString(), out c) == false)
				errors.Add($"Тип данных для product_y должен быть double");
			else
				if (double.Parse(query.product_y.ToString()) < 0)
				errors.Add($"Значение product_y должно быть больше 0");

			if (errors.Count != 0)
				return new CustomBadRequest("При валидации данных произошла ошибка", errors);
			else
				return null;
		}
	}
}
