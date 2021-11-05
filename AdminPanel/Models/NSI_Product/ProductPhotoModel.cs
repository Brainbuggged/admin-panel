using System;

namespace AdminPanel.Models.Models.NSI_Product
{
	public class ProductPhotoModel
	{
		public Guid id { get; set; }
		public string linq { get; set; }
		public bool? is_main { get; set; }
		public Guid productid { get; set; }
		public ProductModel product { get; set; }
	}
}
