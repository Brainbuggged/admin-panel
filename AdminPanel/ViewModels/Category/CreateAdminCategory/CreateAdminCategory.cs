using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.ViewModels.Category.CreateAdminCategory
{
	public class CreateAdminCategory
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
		public IFormFile photo { get; set; }
	}
}
