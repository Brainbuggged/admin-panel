using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using AdminPanel.Models.Category.AddCategory.Query;
using AdminPanel.Models.Category.AddCategoryParameter.Query;
using AdminPanel.Models.Category.AddCategoryParameterValues;
using AdminPanel.Services;
using AdminPanel.Extensions;
using AdminPanel.QueryChecker;

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

	}
}
