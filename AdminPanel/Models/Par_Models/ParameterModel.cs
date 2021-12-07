using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models.Models.Par_Models
{
	[Index("name")]
	public class ParameterModel
	{
		[Display(Name = "Идентификатор")]

		public Guid id { get; set; }
		[Display(Name = "Идентификатор категории")]

		public Guid categoryid { get; set; }
		[Display(Name = "Категория")]

		public CategoryModel category { get; set; }
		[Display(Name = "Название")]

		public string name { get; set; }
		public List<ParameterValueModel> values { get; set; }
	}
}
