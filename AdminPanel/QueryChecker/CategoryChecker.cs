using AdminPanel.Models.Category.AddCategory.Query;
using AdminPanel.Models.Category.AddCategoryParameter.Query;
using AdminPanel.Models.Category.AddCategoryParameterValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminPanel.Extensions;

namespace AdminPanel.QueryChecker
{
	public class CategoryChecker
	{
		public CustomBadRequest Check_AddCategory(PostNewCategory query)
		{
			List<string> errors = new List<string>();

			if (String.IsNullOrWhiteSpace(query.categoryRuName))
				errors.Add($"Параметр categoryRuName явяляется обязательным для заполнения");

			if (errors.Count != 0)
				return new CustomBadRequest("При валидации данных произошла ошибка", errors);
			else
				return null;
		}

		public CustomBadRequest Check_AddCategoryParameter(addCategoryParameter query)
		{
			List<string> errors = new List<string>();

			if (String.IsNullOrWhiteSpace(query.categoryName))
				errors.Add($"Параметр categoryName явяляется обязательным для заполнения");

			if (String.IsNullOrWhiteSpace(query.parameterName))
				errors.Add($"Параметр parameterName явяляется обязательным для заполнения");

			if (errors.Count != 0)
				return new CustomBadRequest("При валидации данных произошла ошибка", errors);
			else
				return null;
		}

		public CustomBadRequest Check_AddCategoryParameterValues(addCategoryParameterValues query)
		{
			List<string> errors = new List<string>();

			if (String.IsNullOrWhiteSpace(query.categoryName))
				errors.Add($"Параметр categoryName явяляется обязательным для заполнения");

			if (String.IsNullOrWhiteSpace(query.parameterName))
				errors.Add($"Параметр parameterName явяляется обязательным для заполнения");

			if (query.values.Count() < 1)
				errors.Add($"Параметр values явяляется обязательным для заполнения");
			else
				if (query.values.FirstOrDefault(item => String.IsNullOrWhiteSpace(item) == true) != null) 
					errors.Add($"Параметр values не должен содержать пустые значения");

			if (errors.Count != 0)
				return new CustomBadRequest("При валидации данных произошла ошибка", errors);
			else
				return null;
		}
	}
}
