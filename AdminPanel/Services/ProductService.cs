using AdminPanel.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPanel.Core.Repositories.NSI_Client;
using AdminPanel.Core.Repositories.NSI_Product;
using AdminPanel.Core.Repositories.NSI_Vendor;
using AdminPanel.Core.Repositories.Par_Models;
using AdminPanel.Models;
using AdminPanel.Models.Catalog.GetCategoryParameters.Response;
using AdminPanel.Models.Models.NSI_Product;
using AdminPanel.Models.Models.NSI_Vendor;
using AdminPanel.Models.Models.Par_Models;
using AdminPanel.ViewModels.Product.CreateDraftProduct.Query;
using AdminPanel.ViewModels.Product.CreateProduct.Query;
using AdminPanel.ViewModels.Product.GetDraftProduct.Response;
using AdminPanel.ViewModels.Product.UpdateProduct.Query;
using AdminPanel.Extensions;


namespace AdminPanel.Services
{
	public class ProductService
	{
		///////////////////////////////////////////////////////////////////////////////////////////
		public async Task<RequestResult> GetProductParameters(Guid clientId, string categoryName)
		{
			var client = await new ClientRepository().GetByIDAsync(clientId);
			if (client == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId} не найден", result = null };
			if (client.role != RoleType.Seller)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId}  не является продавцом", result = null };

			var category = await new CategoryRepository().GetByNameAsync(categoryName);
			if (category == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Категория {categoryName} не существует", result = null };

			var parametersWithValues = new List<ResponseCategoryParameter>();

			var parameters = (await new ParameterRepository().GetAllByCategoryAsync(category.id)).ToList();

			parameters.ForEach(item =>
			{
				parametersWithValues.Add(new ResponseCategoryParameter
				{
					name = item.name,
					values = new ParameterValueRepository().GetByParameterAsync(item.id).ToList()
				});
			});

			return new RequestResult { status = ResultStatus.Ok, message = "", result = parametersWithValues };
		}
		
		
		///////////////////////////////////////////////////////////////////////////////////////////
		public async Task<RequestResult> OpenDraftRequest(Guid clientId, string requestCategory, string requestMessage)
		{
			var client = await new ClientRepository().GetByIDAsync(clientId);
			if (client == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId} не найден", result = null };
			if (client.role != RoleType.Seller)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId}  не является продавцом", result = null };

			var newRequest = new VendorDraftRequestModel
			{
				id = Guid.NewGuid(),
				number = await new VendorDraftRequestRepository().CountRequestsAsync() + 1,
				message = requestMessage,
				category = requestCategory,
				request_date = new SettingsExtension().GetDateTimeNow(),
				vendorid = (Guid)client.vendorid,
				closed_date = null
			};

			await new VendorDraftRequestRepository().AddAsync(newRequest);

			return new RequestResult { status = ResultStatus.Accepted, message = "Запрос успешно отправлен", result = null };
		}
		///////////////////////////////////////////////////////////////////////////////////////////
		public async Task<RequestResult> CloseDraftRequest(Guid clientId, int requestNumber)
		{
			var client = await new ClientRepository().GetByIDAsync(clientId);
			if (client == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId} не найден", result = null };
			if (client.role != RoleType.Seller)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId}  не является продавцом", result = null };

			var request = await new VendorDraftRequestRepository().GetByNumberAsync(requestNumber);
			if (request == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Запрос с номером {requestNumber} не существует", result = null };
			if (request.closed_date != null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Запрос с номером {requestNumber} уже закрыт", result = null };

			await new VendorDraftRequestRepository().CloseAsync(requestNumber);

			return new RequestResult { status = ResultStatus.Accepted, message = $"Запрос с номером {requestNumber} успешно закрыт", result = null };
		}
		///////////////////////////////////////////////////////////////////////////////////////////
		public async Task<RequestResult> GetProductToUpdate(Guid clientId, Guid productId)
		{
			var client = await new ClientRepository().GetByIDAsync(clientId);
			if (client == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId} не найден", result = null };
			if (client.role != RoleType.Seller)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId}  не является продавцом", result = null };

			var product = await new ProductRepository().GetByIdAsync(productId);
			if (product == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Товар с идентификатором {productId} не найден", result = null };
			if (product.vendorid != client.vendorid)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Товар с идентификатором {productId} не является вашим", result = null };
				var productToUpdate = new ResponseProductToUpdate
				{
				product_id = product.id,
				product_number = new TranslitExtension().Run(product.name),
				product_type = product.type.GetText(),
				product_name = product.name,
				product_category = await new ProductCategoryRepository().GetByIDAsync(product.categoryid),
				product_brand = product.brand,
				product_prise = product.prise,
				product_description = product.description,
				product_release_year = product.release_year,
				product_amount = product.count,
				product_is_pickuped = (bool)product.is_pickuped,
				product_address = product.address,
				product_country = product.country,
				product_city = product.city,
				product_x = product.x,
				product_y = product.y,
				product_is_delivered = (bool)product.is_delivery_expected,

				product_photoes = (List<createProductPhoto>)await new ProductPhotoesRepository().GetDraftProductAsync(product.id),
				product_properties = (List<createProductProperty>)await new ProductPropertiesRepository().GetDraftPropertiesAsync(product.id),
			};

			return new RequestResult { status = ResultStatus.Ok, message = "", result = productToUpdate };
		}
		///////////////////////////////////////////////////////////////////////////////////////////
		public async Task<RequestResult> ArchiveProduct(Guid clientId, Guid productId)
		{
			var client = await new ClientRepository().GetByIDAsync(clientId);
			if (client == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId} не найден", result = null };
			if (client.role != RoleType.Seller)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId}  не является продавцом", result = null };

			var product = await new ProductRepository().GetByNumberAndVendorAsync(productId, (Guid)client.vendorid);
			if (product == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Товар с идентификатором {productId} не существует", result = null };
			if (product.status == ProductStatus.Archive)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Товар с идентификатором {productId} уже находится в архиве", result = null };

			await new ProductRepository().ChangeProductStatusAsync(product.id, ProductStatus.Archive);

			return new RequestResult { status = ResultStatus.Accepted, message = $"Товар с идентификатором {productId} успешно добавлен в архив", result = null };
		}
		///////////////////////////////////////////////////////////////////////////////////////////
		public async Task<RequestResult> DeactivateProduct(Guid clientId, Guid productId)
		{
			var client = await new ClientRepository().GetByIDAsync(clientId);
			if (client == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId} не найден", result = null };
			if (client.role != RoleType.Seller)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId}  не является продавцом", result = null };

			var product = await new ProductRepository().GetByNumberAndVendorAsync(productId, (Guid)client.vendorid);
			if (product == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Товар с идентификатором {productId} не существует", result = null };
			if (product.status == ProductStatus.Snyat)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Товар с идентификатором {productId} уже снят", result = null };

			await new ProductRepository().ChangeProductStatusAsync(product.id, ProductStatus.Snyat);

			return new RequestResult { status = ResultStatus.Accepted, message = $"Товар с идентификатором {productId} успешно снят с продажи", result = null };
		}
		///////////////////////////////////////////////////////////////////////////////////////////
		public async Task<RequestResult> ActivateProduct(Guid clientId, Guid productId)
		{
			var client = await new ClientRepository().GetByIDAsync(clientId);
			if (client == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId} не найден", result = null };
			if (client.role != RoleType.Seller)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId}  не является продавцом", result = null };

			var product = await new ProductRepository().GetByNumberAndVendorAsync(productId, (Guid)client.vendorid);
			if (product == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Товар с идентификатором {productId} не существует", result = null };
			if (product.status == ProductStatus.Vistavlen)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Товар с идентификатором {productId} уже выставлен", result = null };

			await new ProductRepository().ChangeProductStatusAsync(product.id, ProductStatus.Vistavlen);

			return new RequestResult { status = ResultStatus.Accepted, message = $"Товар с идентификатором {productId} успешно выставлен на продажу", result = null };
		}
		///////////////////////////////////////////////////////////////////////////////////////////
		public async Task<RequestResult> ChangeMyProductCount(Guid clientId, Guid productId, int productCount)
		{
			var client = await new ClientRepository().GetByIDAsync(clientId);
			if (client == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId} не найден", result = null };
			if (client.role != RoleType.Seller)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId}  не является продавцом", result = null };

			var product = await new ProductRepository().GetByNumberAndVendorAsync(productId, (Guid)client.vendorid);
			if (product == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Товар с идентификатором {productId} не существует", result = null };
			if (product.type == ProductType.BU && productCount > 1)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Товар с идентификатором {productId} является Б/У и может быть в количество от 0 до 1", result = null };

			await new ProductRepository().ChangeProductCountAsync(product.id, productCount);

			return new RequestResult { status = ResultStatus.Accepted, message = $"Количество товара с идентификатором {productId} успешно изменено", result = null };
		}
		///////////////////////////////////////////////////////////////////////////////////////////
		public async Task<RequestResult> ChangeMyProductDelivery(Guid clientId, Guid productId)
		{
			var client = await new ClientRepository().GetByIDAsync(clientId);
			if (client == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId} не найден", result = null };
			if (client.role != RoleType.Seller)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId}  не является продавцом", result = null };

			var product = await new ProductRepository().GetByNumberAndVendorAsync(productId, (Guid)client.vendorid);
			if (product == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Товар с идентификатором {productId} не найден", result = null };
			if (product.count != 0)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Товар с идентификатором {productId} больше 0, поставка невозможна", result = null };

			await new ProductRepository().ChangeProductDeliveryAsync(product.id, !product.is_delivery_expected);

			return new RequestResult { status = ResultStatus.Accepted, message = "Установлен параметр 'товар ожидает поставки'", result = null };
		}
		///////////////////////////////////////////////////////////////////////////////////////////
		public async Task<RequestResult> GetMyProducts(Guid clientId)
		{
			var client = await new ClientRepository().GetByIDAsync(clientId);
			if (client == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId} не найден", result = null };
			if (client.role != RoleType.Seller)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId}  не является продавцом", result = null };

			var myProducts = (await new ProductRepository().GetMyProductsAsync((Guid)client.vendorid)).ToList();
			myProducts.ForEach(prod =>
			{
				prod.product_type = ((ProductType)int.Parse(prod.product_type)).GetText();
				prod.product_status = ((ProductStatus)int.Parse(prod.product_status)).GetText();
			});

			return new RequestResult { status = ResultStatus.Ok, message = "", result = myProducts };
		}
		///////////////////////////////////////////////////////////////////////////////////////////
		public async Task<RequestResult> GetMyDraftProducts(Guid clientId)
		{
			var client = await new ClientRepository().GetByIDAsync(clientId);
			if (client == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId} не найден", result = null };
			if (client.role != RoleType.Seller)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId}  не является продавцом", result = null };

			var myProducts = (await new ProductRepository().GetMyDraftProductsAsync((Guid)client.vendorid)).ToList();
			myProducts.ForEach(prod =>
			{
				prod.product_type = ((ProductType)int.Parse(prod.product_type)).GetText();
				prod.product_status = ((ProductStatus)int.Parse(prod.product_status)).GetText();
			});

			return new RequestResult { status = ResultStatus.Ok, message = "", result = myProducts };
		}
		///////////////////////////////////////////////////////////////////////////////////////////
		public async Task<RequestResult> GetMyArchiveProducts(Guid clientId)
		{
			var client = await new ClientRepository().GetByIDAsync(clientId);
			if (client == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId} не найден", result = null };
			if (client.role != RoleType.Seller)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId}  не является продавцом", result = null };

			var myProducts = (await new ProductRepository().GetMyArchiveProductsAsync((Guid)client.vendorid)).ToList();
			myProducts.ForEach(prod =>
			{
				prod.product_type = ((ProductType)int.Parse(prod.product_type)).GetText();
				prod.product_status = ((ProductStatus)int.Parse(prod.product_status)).GetText();
			});

			return new RequestResult { status = ResultStatus.Ok, message = "", result = myProducts };
		}
		///////////////////////////////////////////////////////////////////////////////////////////
		public async Task<RequestResult> UpdateProduct(Guid clientId, updateProduct updateProduct)
		{
			var client = await new ClientRepository().GetByIDAsync(clientId);
			if (client == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId} не найден", result = null };
			if (client.role != RoleType.Seller)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Клиент с идентификатором {clientId}  не является продавцом", result = null };

			var vendor = await new VendorRepository().GetVendorByIdAsync((Guid)client.vendorid);
			///
			var par_category = await new CategoryRepository().GetByNameAsync(updateProduct.product_category);

			var checkProduct = await new NewObjectsChecker().CheckUpdateProduct(updateProduct);
			if (checkProduct.status == ResultStatus.BadRequest)
				return new RequestResult { status = ResultStatus.BadRequest, message = checkProduct.message, result = null };
			///
			updateProduct = (updateProduct)checkProduct.result;

			var product = await new ProductRepository().GetByNumberAndVendorAsync(updateProduct.product_id, (Guid)client.vendorid);
			if (product == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Товар с идентификатором {updateProduct.product_id} не является вашим", result = null };

			await new ProductRepository().UpdateAsync(updateProduct);
			await new ProductPropertiesRepository().RemoveByProductIdAsync(product.id);
			await new ProductPhotoesRepository().RemoveByProductIdAsync(product.id);
			///////
			var par_parameters = (await new ParameterRepository().GetIdByCategoryAsync(par_category.id)).ToList();
			///////
			updateProduct.product_properties.ForEach(prop =>
			{
				var parameterId = par_parameters.Single(item => item.name == prop.property_name).id;
				if (new ParameterValueRepository().GetByValueAndParameter(parameterId, prop.property_value) == null)
				{
					new ParameterValueRepository().Add(new ParameterValueModel
					{
						id = Guid.NewGuid(),
						parameterid = parameterId,
						value = prop.property_value
					});
				}
				new ProductPropertiesRepository().Add(new ProductPropertyModel
				{
					id = Guid.NewGuid(),
					name = prop.property_name,
					productid = product.id,
					value = prop.property_value,
					alternative_value = ""
				});
			});
			updateProduct.product_photoes.ForEach(photo =>
			{
				new ProductPhotoesRepository().Add(new ProductPhotoModel
				{
					id = Guid.NewGuid(),
					is_main = photo.is_main,
					linq = photo.linq,
					productid = product.id
				});
			});
			return new RequestResult { status = ResultStatus.Accepted, message = "Товар успешно изменен", result = null };
		}

	}
}
