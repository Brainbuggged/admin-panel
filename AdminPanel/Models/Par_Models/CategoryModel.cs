using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models.Models.Par_Models
{
	[Index("name", "en_name")]
	public class CategoryModel
	{
		[Display(Name = "Идентификатор")]
		public Guid id { get; set; }
		[Display(Name = "Название")]

		public string name { get; set; }
		[Display(Name = "Название на английском")]

		public string en_name { get; set; }
	}
}
