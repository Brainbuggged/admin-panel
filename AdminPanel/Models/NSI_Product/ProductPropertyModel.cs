using Microsoft.EntityFrameworkCore;
using System;

namespace AdminPanel.Models.Models.NSI_Product
{
	[Index("name")]
	public class ProductPropertyModel
	{
		public Guid id { get; set; }
		public string name { get; set; }
		public string value { get; set; }
		public string alternative_value { get; set; }
		public Guid productid { get; set; }
		public ProductModel product { get; set; }
	}
}
