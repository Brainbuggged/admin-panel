using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdminPanel.Core.Repositories.NSI_Product;
using AdminPanel.Core.Repositories.Par_Models;
using AdminPanel.Models.Category.AddCategory.Query;
using AdminPanel.Models.Models.NSI_Product;
using AdminPanel.Models.Models.Par_Models;
using AdminPanel.Extensions;

namespace AdminPanel.Services
{
	public class CategoryService
	{
		
		public async Task<RequestResult> AddCategory(PostNewCategory newCategory)
		{
			var category = await new ProductCategoryRepository().GetByNameAsync(newCategory.categoryRuName);
			if (category != null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Категория с названием {newCategory.categoryRuName} уже сущевтует", result = null };

			ProductCategoryModel parentCategory = null;
			if (!String.IsNullOrWhiteSpace(newCategory.parentCategoryRuName))
			{
				parentCategory = await new ProductCategoryRepository().GetByNameAsync(newCategory.parentCategoryRuName);
				if (parentCategory != null)
					return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Родительская категория с названием {newCategory.parentCategoryRuName} уже сущевтует", result = null };
			}
			var addingCategory = new ProductCategoryModel
			{
				id = Guid.NewGuid(),
				ru_name = newCategory.categoryRuName.ToUpper(),
				en_name = new TranslitExtension().Run(newCategory.categoryRuName),
				parentid = parentCategory != null ? parentCategory.id.ToString() : "",
				is_last = true,
				photo = ""
			};

			await new ProductCategoryRepository().AddAsync(addingCategory);

			if (parentCategory != null)
				await new ProductCategoryRepository().UpdateParentAsync(parentCategory.id);

			var parCategory = new CategoryModel
			{
				id = Guid.NewGuid(),
				name = addingCategory.ru_name,
				en_name = addingCategory.en_name
			};

			await new CategoryRepository().AddAsync(parCategory);

			foreach (var par in newCategory.parameters)
				await new ParameterRepository().AddAsync(new ParameterModel
				{
					id = Guid.NewGuid(),
					name = par,
					categoryid = parCategory.id
				});

			return new RequestResult { status = ResultStatus.Accepted, message = $"Категория {newCategory.categoryRuName}/{addingCategory.en_name} успешно добавлена", result = null };
		}
		
		/////////////////////////////////////////////////////////////////////////////
		public async Task<RequestResult> AddCategoryParameter(string categoryName, string parameterName)
		{
			var category = await new CategoryRepository().GetByNameAsync(categoryName);
			if (category == null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Категория с названием {categoryName} не сущевтует", result = null };

			if (await new ParameterRepository().GetByNameAndCategoryAsync(category.id, parameterName) != null)
				return new RequestResult { status = ResultStatus.UnprocessableEntity, message = $"Категория с названием {categoryName} уже содержит параметр {parameterName}", result = null };

			var catParameter = new ParameterModel
			{
				id = Guid.NewGuid(),
				name = parameterName,
				categoryid = category.id
			};

			await new ParameterRepository().AddAsync(catParameter);
			return new RequestResult { status = ResultStatus.Accepted, message = $"Параметр {parameterName} успешно добавлен", result = null };
		}
	}
}
