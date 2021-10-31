using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPanel.Models.Catalog.GetCatalogProducts.Query;
using AdminPanel.Models.Catalog.GetCategories.Query;
using AdminPanel.Models.Catalog.GetCategoryParameters.Query;
using AdminPanel.Models.Catalog.GetProductCard.Query;
using AdminPanel.Models.Catalog.GetVendorCard.Query;
using AdminPanel.Services;
using AdminPanel.ViewModels.Catalog.GetChat.Query;
using AdminPanel.ViewModels.Catalog.SendChatMessage.Query;
using AdminPanel.Extensions;
using AdminPanel.Extensions;
using AdminPanel.QueryChecker;

namespace AdminPanel.WEB.Controllers
{
	[AllowAnonymous]
	[Route("api/[controller]")]
	[ApiController]

	public class CatalogController : ControllerBase
	{
		private readonly CatalogService catalogService;
		public CatalogController()
		{
			catalogService = new CatalogService();
		}

		/// <summary>
		/// Возвращает список подкатегорий к запрашиваемой. Поиск идет как по русскому, так и по английскому наименованию по точному совпадению
		/// </summary>
		[Route("get-categories")]
		[HttpGet]
		[Produces("application/json")]
		public async Task<IActionResult> GetCategories([FromQuery] getCategories query)
		{
			try
			{
				var result = await catalogService.GetCategories(query.categoryName);

				return new ObjectResultCreator().CreateObjectResult(result);
			}
			catch (Exception ex)
			{
				return new ObjectResult(new CustomInternalServerError("Что-то пошло не так в /get-categories", ex.Message));
			}
		}
		
		/// <summary>
		/// Возвращает список всех-категорий
		/// </summary>
		[Route("get-all-categories")]
		[HttpGet]
		[Produces("application/json")]
		public async Task<IActionResult> GetAllCategories()
		{
			try
			{
				var result = await catalogService.GetAllCategories();

				return new ObjectResultCreator().CreateObjectResult(result);
			}
			catch (Exception ex)
			{
				return new ObjectResult(new CustomInternalServerError("Что-то пошло не так в /get-all-categories"+ ex.StackTrace, ex.Message ));
			}
		}


		/// <summary>
		/// Возвращает подробную информацию по запрашивамомой карточке товара
		/// </summary>
		[Route("get-product-card")]
		[HttpGet]
		[Produces("application/json")]
		public async Task<IActionResult> GetProductCard([FromQuery] getProductCard query)
		{
			try
			{
				Guid clientId = Guid.Empty;
				if (HttpContext.User.Identity.IsAuthenticated)
					clientId = Guid.Parse(HttpContext.User.Claims.First(item => item.Type == "id").Value);

				var validator = new CatalogChecker().Check_GetProductCard(query);
				if (validator != null)
					return new ObjectResult(validator);

				var result = await catalogService.GetProductCard(clientId, query.productNumber);

				return new ObjectResultCreator().CreateObjectResult(result);
			}
			catch (Exception ex)
			{
				return new ObjectResult(new CustomInternalServerError("Что-то пошло не так в /get-product-card", ex.Message));
			}
		}

		/// <summary>
		/// Возвращает подробную информацию по запрашивамомой карточке продавца
		/// </summary>
		[Route("get-vendor-card")]
		[HttpGet]
		[Produces("application/json")]
		public async Task<IActionResult> GetVendorCard([FromQuery] getVendorCard query)
		{
			try
			{
				Guid clientId = Guid.Empty;
				if (HttpContext.User.Identity.IsAuthenticated)
					clientId = Guid.Parse(HttpContext.User.Claims.First(item => item.Type == "id").Value);

				var validator = new CatalogChecker().Check_GetVendorCard(query);
				if (validator != null)
					return new ObjectResult(validator);

				var result = await catalogService.GetVendorCard(clientId, query.vendorNumber);

				return new ObjectResultCreator().CreateObjectResult(result);
			}
			catch (Exception ex)
			{
				return new ObjectResult(new CustomInternalServerError("Что-то пошло не так в /get-vendor-card", ex.Message));
			}
		}
	}
}
