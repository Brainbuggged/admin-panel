using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AdminPanel.Services;
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
using AdminPanel.Extensions;
using AdminPanel.Extensions;
using AdminPanel.QueryChecker;
using AdminPanel.WEB;

namespace AdminPanel.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private ProductService productService;
		public ProductController()
		{
			productService = new ProductService();
		}

		/// <summary>
		/// Вернуть список технических характеристик для категории
		/// </summary>
		[Route("get-product-parameters")]
		[HttpGet]
		[Produces("application/json")]
		public async Task<IActionResult> GetProductParameters([FromQuery] getProductParameters query)
		{
			try
			{
				var validator = new ProductChecker().Check_GetProductParameters(query);
				if (validator != null)
					return new ObjectResult(validator);

				var clientId = Guid.Parse(HttpContext.User.Claims.First(item => item.Type == "id").Value);

				var result = await productService.GetProductParameters(clientId, query.categoryName);

				return new ObjectResultCreator().CreateObjectResult(result);
			}
			catch (Exception ex)
			{
				return new ObjectResult(new CustomInternalServerError("Что-то пошло не так в /create-product", ex.Message));
			}
		}



		/// <summary>
		/// Отправить запрос на параметры
		/// </summary>
		[Route("open-draft-request")]
		[HttpPost]
		[Produces("application/json")]
		public async Task<IActionResult> OpenDraftRequest([FromBody] openDraftRequest query)
		{
			try
			{
				var validator = new ProductChecker().Check_OpenDraftRequest(query);
				if (validator != null)
					return new ObjectResult(validator);

				var clientId = Guid.Parse(HttpContext.User.Claims.First(item => item.Type == "id").Value);

				var result = await productService.OpenDraftRequest(clientId, query.category, query.message);

				return new ObjectResultCreator().CreateObjectResult(result);
			}
			catch (Exception ex)
			{
				return new ObjectResult(new CustomInternalServerError("Что-то пошло не так в /open-draft-request", ex.Message));
			}
		}

		/// <summary>
		/// Закрыть запрос на параметры
		/// </summary>
		[Route("close-draft-request")]
		[HttpPut]
		[Produces("application/json")]
		public async Task<IActionResult> CloseDraftRequest([FromBody] closeDraftRequest query)
		{
			try
			{
				var validator = new ProductChecker().Check_CloseDraftRequest(query);
				if (validator != null)
					return new ObjectResult(validator);

				var clientId = Guid.Parse(HttpContext.User.Claims.First(item => item.Type == "id").Value);

				var result = await productService.CloseDraftRequest(clientId, query.requestNumber);

				return new ObjectResultCreator().CreateObjectResult(result);
			}
			catch (Exception ex)
			{
				return new ObjectResult(new CustomInternalServerError("Что-то пошло не так в /close-draft-request", ex.Message));
			}
		}

		/// <summary>
		/// Вернуть черновой товар
		/// </summary>
		[Route("get-product-to-update")]
		[HttpGet]
		[Produces("application/json")]
		public async Task<IActionResult> GetProductToUpdate([FromQuery] getProductToUpdate query)
		{
			try
			{
				var validator = new ProductChecker().Check_GetProductToUpdate(query);
				if (validator != null)
					return new ObjectResult(validator);

				var clientId = Guid.Parse(HttpContext.User.Claims.First(item => item.Type == "id").Value);
				var result = await productService.GetProductToUpdate(clientId, query.productId);

				return new ObjectResultCreator().CreateObjectResult(result);
			}
			catch (Exception ex)
			{
				return new ObjectResult(new CustomInternalServerError("Что-то пошло не так в /get-product-to-update", ex.Message));
			}
		}

		/// <summary>
		/// Удалить черновой товар
		/// </summary>
		[Route("archive-product")]
		[HttpPut]
		[Produces("application/json")]
		public async Task<IActionResult> ArchiveProduct([FromQuery] archiveProduct query)
		{
			try
			{
				var validator = new ProductChecker().Check_ArchiveProduct(query);
				if (validator != null)
					return new ObjectResult(validator);

				var clientId = Guid.Parse(HttpContext.User.Claims.First(item => item.Type == "id").Value);

				var result = await productService.ArchiveProduct(clientId, query.productId);

				return new ObjectResultCreator().CreateObjectResult(result);
			}
			catch (Exception ex)
			{
				return new ObjectResult(new CustomInternalServerError("Что-то пошло не так в /archive-product", ex.Message));
			}
		}

		/// <summary>
		/// Выставить товар
		/// </summary>
		[Route("activate-product")]
		[HttpPut]
		[Produces("application/json")]
		public async Task<IActionResult> ActivateProduct([FromQuery] activateProduct query)
		{
			try
			{

				var validator = new ProductChecker().Check_ActivateProduct(query);
				if (validator != null)
					return new ObjectResult(validator);

				var clientId = Guid.Parse(HttpContext.User.Claims.First(item => item.Type == "id").Value);

				var result = await productService.ActivateProduct(clientId, query.productId);

				return new ObjectResultCreator().CreateObjectResult(result);
			}
			catch (Exception ex)
			{
				return new ObjectResult(new CustomInternalServerError("Что-то пошло не так в /activate-product", ex.Message));
			}
		}

		/// <summary>
		/// Снять товар товар
		/// </summary>
		[Route("deactivate-product")]
		[HttpPut]
		[Produces("application/json")]
		public async Task<IActionResult> DeactivateProduct([FromQuery] deActivateProduct query)
		{
			try
			{
				var validator = new ProductChecker().Check_DeactivateProduct(query);
				if (validator != null)
					return new ObjectResult(validator);

				var clientId = Guid.Parse(HttpContext.User.Claims.First(item => item.Type == "id").Value);

				var result = await productService.DeactivateProduct(clientId, query.productId);

				return new ObjectResultCreator().CreateObjectResult(result);
			}
			catch (Exception ex)
			{
				return new ObjectResult(new CustomInternalServerError("Что-то пошло не так в /deactivate-product", ex.Message));
			}
		}

		/// <summary>
		/// Вернуть список моих товаров
		/// </summary>
		[Route("get-my-products")]
		[HttpGet]
		[Produces("application/json")]
		public async Task<IActionResult> GetMyProducts()
		{
			try
			{
				var clientId = Guid.Parse(HttpContext.User.Claims.First(item => item.Type == "id").Value);

				var result = await productService.GetMyProducts(clientId);

				return new ObjectResultCreator().CreateObjectResult(result);
			}
			catch (Exception ex)
			{
				return new ObjectResult(new CustomInternalServerError("Что-то пошло не так в /get-my-products", ex.Message));
			}
		}

		/// <summary>
		/// Вернуть список моих черновых товаров
		/// </summary>
		[Route("get-my-draft-products")]
		[HttpGet]
		[Produces("application/json")]
		public async Task<IActionResult> GetMyDraftProducts()
		{
			try
			{

				var clientId = Guid.Parse(HttpContext.User.Claims.First(item => item.Type == "id").Value);

				var result = await productService.GetMyDraftProducts(clientId);

				return new ObjectResultCreator().CreateObjectResult(result);
			}
			catch (Exception ex)
			{
				return new ObjectResult(new CustomInternalServerError("Что-то пошло не так в /get-my-draft-products", ex.Message));
			}
		}

		/// <summary>
		/// Вернуть список моих архивных товаров
		/// </summary>
		[Route("get-my-archive-products")]
		[HttpGet]
		[Produces("application/json")]
		public async Task<IActionResult> GetMyArchiveProducts()
		{
			try
			{

				var clientId = Guid.Parse(HttpContext.User.Claims.First(item => item.Type == "id").Value);

				var result = await productService.GetMyArchiveProducts(clientId);

				return new ObjectResultCreator().CreateObjectResult(result);
			}
			catch (Exception ex)
			{
				return new ObjectResult(new CustomInternalServerError("Что-то пошло не так в /get-my-archive-products", ex.Message));
			}
		}

		/// <summary>
		/// Изменить количество товара
		/// </summary>
		[Route("change-my-product-count")]
		[HttpPut]
		[Produces("application/json")]
		public async Task<IActionResult> ChangeMyProductCount([FromQuery] changeMyProductCount query)
		{
			try
			{

				var validator = new ProductChecker().Check_ChangeMyProductCount(query);
				if (validator != null)
					return new ObjectResult(validator);

				var clientId = Guid.Parse(HttpContext.User.Claims.First(item => item.Type == "id").Value);

				var result = await productService.ChangeMyProductCount(clientId, query.productId, query.productCount);

				return new ObjectResultCreator().CreateObjectResult(result);
			}
			catch (Exception ex)
			{
				return new ObjectResult(new CustomInternalServerError("Что-то пошло не так в /change-my-product-count", ex.Message));
			}
		}
		
		/// <summary>
		/// Изменить количество товара
		/// </summary>
		[Route("change-my-product-delivery")]
		[HttpPut]
		[Produces("application/json")]
		public async Task<IActionResult> ChangeMyProductDelivery([FromQuery] changeMyProductDelivery query)
		{
			try
			{
				var validator = new ProductChecker().Check_ChangeMyProductDelivery(query);
				if (validator != null)
					return new ObjectResult(validator);

				var clientId = Guid.Parse(HttpContext.User.Claims.First(item => item.Type == "id").Value);

				var result = await productService.ChangeMyProductDelivery(clientId, query.productId);

				return new ObjectResultCreator().CreateObjectResult(result);
			}
			catch (Exception ex)
			{
				return new ObjectResult(new CustomInternalServerError("Что-то пошло не так в /change-my-product-delivery", ex.Message));
			}
		}

		/// <summary>
		/// Изменение товара в системе
		/// </summary>
		[Route("update-product")]
		[HttpPut]
		[Produces("application/json")]
		public async Task<IActionResult> UpdateProduct([FromBody] updateProduct query)
		{
			try
			{
				var validator = new ProductChecker().Check_UpdateProduct(query);
				if (validator != null)
					return new ObjectResult(validator);

				var clientId = Guid.Parse(HttpContext.User.Claims.First(item => item.Type == "id").Value);
				var result = await productService.UpdateProduct(query);

				return new ObjectResultCreator().CreateObjectResult(result);
			}
			catch (Exception ex)
			{
				return new ObjectResult(new CustomInternalServerError("Что-то пошло не так в /update-product", ex.Message));
			}
		}
	}
}
