using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models.Models.NSI_Product
{
	[Index("ru_name", "en_name")]
	public class ProductCategoryModel
	{
		[Display(Name = "Идентификатор")]
		public Guid id { get; set; }
		[Display(Name = "Родитель")]
		public string parentid { get; set; }
		[Display(Name = "Наименование")]
		public string ru_name { get; set; }
		[Display(Name = "Наименование на английском")]
		public string en_name { get; set; }
		[Display(Name = "Является ли подкатегорией")]
		public bool? is_last { get; set; }
		[Display(Name = "Фото")]
		public string photo { get; set; }
	}
}
