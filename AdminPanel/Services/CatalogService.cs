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
using AdminPanel.Models.Catalog.GetCatalogProducts.Query;
using AdminPanel.Models.Catalog.GetCatalogProducts.Response;
using AdminPanel.Models.Catalog.GetCategories.Response;
using AdminPanel.Models.Catalog.GetCategoryParameters.Response;
using AdminPanel.Models.Catalog.GetProductCard.Response;
using AdminPanel.Models.Catalog.GetVendorCard.Response;
using AdminPanel.Models.Models.NSI_Client;
using AdminPanel.ViewModels.Catalog.GetChat.Response;
using AdminPanel.Extensions;


namespace AdminPanel.Services
{
	public class CatalogService
	{

		public async Task<RequestResult> GetCategories(string categoryName)
		{
			List<ResponseCategory> _categories = new List<ResponseCategory>();

			if (String.IsNullOrWhiteSpace(categoryName))
				_categories = (List<ResponseCategory>)await new ProductCategoryRepository().GetAllAsync();
			else
			{
				var car = await new ProductCategoryRepository().GetByNameAsync(categoryName);
				if (car == null)
					return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Категория {categoryName} не существует", result = null };

				_categories = (List<ResponseCategory>)await new ProductCategoryRepository().GetChildrenAsync(car.id);
			}

			return new RequestResult { status = ResultStatus.Ok, message = "", result = _categories };
		}
		///////////////////////////////////////////////////////////////////////////////////////////
		public async Task<RequestResult> GetVendorCard(Guid clientId, string vendorNumber)
		{
			var vendor = await new VendorRepository().GetByNumberAsync(vendorNumber);
			if (vendor == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Продавец с номером {vendorNumber} не найден", result = "" };


			var commentCount = await new VendorRepository().CountCommentsAsync(vendor.id);
			var vendorComments = (List<ResponseVendorComment>)await new ProductCommentsRepository().GetByVendorAsync(vendor.id);
			var vendorProducts = (List<ResponseVendorProduct>)await new ProductRepository().GetProductsByVendorAsync(clientId, vendor.id);
			var vendorSocials = (List<ResponseSocialMedia>)await new VendorSocialRepository().GetByVendorAsync(vendor.id);

			var _response = new ResponseVendorCard
			{
				vendorInfo = new ResponseVendorInfo
				{
					vendor_fio = $"{vendor.surname} {vendor.name} {vendor.patronymic}",
					is_fiz_face = (bool)vendor.is_fiz_face,
					is_ur_face = (bool)vendor.is_ur_face,
					vendor_phone = vendor.phone,
					vendor_rating = vendor.rating,
					comment_count = commentCount,
					vendor_id = vendor.id,
					is_in_favourite = await new FavouriteVendorRepository().CheckInFavouriteAsync(vendor.id, clientId),
				},
				vendorComments = vendorComments,
				vendorProducts = vendorProducts,
				vendorSocials = vendorSocials
			};
			_response.vendorProducts.ForEach(item => item.product_type = ((ProductType)int.Parse(item.product_type)).GetText());
			_response.vendorSocials.ForEach(item => item.name = ((SocialType)int.Parse(item.name)).GetText());

			return new RequestResult { status = ResultStatus.Ok, message = "", result = _response };
		}
		///////////////////////////////////////////////////////////////////////////////////////////
		public async Task<RequestResult> GetCategoryParameters(string categoryName)
		{
			var category = await new CategoryRepository().GetByNameAsync(categoryName);
			if (category == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Категория {categoryName} не существует", result = null };

			var parametersWithValues = new List<ResponseCategoryParameter>();

			var parameters = (List<string>)await new ParameterRepository().GetByCategoryAsync(category.id);

			parameters.ForEach(item =>
			{
				if (item == "Бренд")
				{
					parametersWithValues.Add(new ResponseCategoryParameter
					{
						name = item,
						values = (List<string>)new ProductRepository().GetBrandList(categoryName)
					});
				}
				else
				{
					parametersWithValues.Add(new ResponseCategoryParameter
					{
						name = item,
						values = (List<string>)new ProductPropertiesRepository().GetByParameterAndCategory(item, categoryName)
					});
				}
			});

			parametersWithValues.Add(new ResponseCategoryParameter
			{
				name = "Город",
				values = (List<string>)new ProductRepository().GetCityList(categoryName)
			});

			return new RequestResult { status = ResultStatus.Ok, message = "", result = parametersWithValues };
		}
	
		///////////////////////////////////////////////////////////////////////////////////////////
		public async Task<RequestResult> GetProductCard(Guid clientId, string productNumber)
		{
			var product = await new ProductRepository().GetByNumberAsync(productNumber);
			if (product == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Товар с номером {productNumber} не найден", result = "" };
			if (new SettingsExtension().ListAvailableProductStatuses().Contains(product.status) == false)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Товар с номером {productNumber} не доступен для просмотра", result = "" };

			var _response = new ResponseProductCard
			{
				productInfo = new ResponseProductInfo
				{
					productId = product.id,
					productName = product.name,
					productType = product.type.GetText(),
					productBrand = product.brand,
					productRating = product.rating,
					productPrise = product.prise,
					productCount = product.count,
					isDeliveryExpected = (bool)product.is_delivery_expected,
					productDescription = product.description,
					productAddress = product.address,
					X = product.x,
					Y = product.y,
					productPublicationDate = product.publication_date.ToShortDateString(),
					is_in_cart = await new CartItemRepository().CheckInCartAsync(product.id, clientId),
					is_in_favourite = await new FavouriteProductRepository().CheckInFavouriteAsync(product.id, clientId),

					productCategory = await new ProductCategoryRepository().GetByIDAsync(product.categoryid),
					productSizes = await new ProductSizesRepository().GetDefaultAsync(),
					productProperties = (List<ResponseProductProperty>)await new ProductPropertiesRepository().GetByProductAsync(product.id),
					productPhotoes = (List<ResponseProductPhoto>)await new ProductPhotoesRepository().GetByProductAsync(product.id),
					productComments = (List<ResponseProductComment>)await new ProductCommentsRepository().GetByProductAsync(product.id)
				},
				vendorInfo = await new VendorRepository().GetByIdAsync(product.vendorid)
			};

			return new RequestResult { status = ResultStatus.Ok, message = "", result = _response };
		}
	}
}
