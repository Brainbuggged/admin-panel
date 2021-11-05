using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using AdminPanel.Core.Repositories.NSI_Product;
using AdminPanel.Core.Repositories.Par_Models;
using AdminPanel.Models.Category.AddCategory.Query;
using AdminPanel.Models.Category.AddCategoryParameter.Query;
using AdminPanel.Models.Category.AddCategoryParameterValues;
using AdminPanel.Services;
using AdminPanel.Extensions;
using AdminPanel.Models.Models.NSI_Product;
using AdminPanel.Models.Models.Par_Models;
using AdminPanel.QueryChecker;
using AdminPanel.WEB;

namespace AdminPanel.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly CategoryService categoryService;
		public CategoryController()
		{
			categoryService = new CategoryService();
		}
		
		/// <summary>
		/// Добавляет новую категорию c параметрами в систему
		/// </summary>
		/// <param name="newCategory">Наименование новой категории со списком параметров</param>
		[Route("add-category")]
		[HttpPost]
		public async Task<IActionResult> AddCategory([FromBody] PostNewCategory newCategory)
		{
			try
			{
				var validator = new CategoryChecker().Check_AddCategory(newCategory);
				if (validator != null)
					return new ObjectResult(validator);

				var result = await categoryService.AddCategory(newCategory);

				return new ObjectResultCreator().CreateObjectResult(result);
			}
			catch (Exception ex)
			{
				return new ObjectResult(new CustomInternalServerError("Что-то пошло не так в /add-category", ex.Message));
			}
		}
		/// <summary>
		/// Добавляет новый параметр к выбранной категории
		/// </summary>
		[Route("add-category-parameter")]
		[HttpPost]
		public async Task<IActionResult> AddCategoryParameter([FromBody] addCategoryParameter query)
		{
			try
			{
				if (!HttpContext.User.Identity.IsAuthenticated)
					return new ObjectResult(new CustomUnauthorized("Доступ запрещен. Требуется авторизация."));

				var validator = new CategoryChecker().Check_AddCategoryParameter(query);
				if (validator != null)
					return new ObjectResult(validator);

				var clientId = Guid.Parse(HttpContext.User.Claims.First(item => item.Type == "id").Value);
				var result = await categoryService.AddCategoryParameter(query.categoryName, query.parameterName);

				return new ObjectResultCreator().CreateObjectResult(result);
			}
			catch (Exception ex)
			{
				return new ObjectResult(new CustomInternalServerError("Что-то пошло не так в /add-category-parameter", ex.Message));
			}
		}

	}
	
	
}
