using System;
using AdminPanel.Models.Models.NSI_Product;

namespace AdminPanel.Models.Models.NSI_Order
{
	public class OrderProductModel
	{
		public Guid id { get; set; }
		public string product_id { get; set; }
		public string product_number { get; set; }
		public DateTime publication_date { get; set; }
		public string photo { get; set; }
		public string name { get; set; }
		public string country { get; set; }
		public string city { get; set; }
		public string category_name { get; set; }
		public string brand { get; set; }
		public double prise { get; set; }

		public int count { get; set; }
		public double total_prise { get; set; }

		public Guid orderid { get; set; }
		public OrderModel order { get; set; }

		public Guid categoryid { get; set; }
		public ProductCategoryModel category { get; set; }
	}
}
