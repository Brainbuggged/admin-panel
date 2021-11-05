using Microsoft.EntityFrameworkCore;
using System;

namespace AdminPanel.Models.Models.NSI_Product
{
	[Index("ru_name", "en_name")]
	public class ProductCategoryModel
	{
		public Guid id { get; set; }
		public string parentid { get; set; }
		public string ru_name { get; set; }
		public string en_name { get; set; }
		public bool? is_last { get; set; }
		public string photo { get; set; }
	}
}
