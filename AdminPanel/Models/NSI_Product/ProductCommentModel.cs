using System;
using AdminPanel.Models.Models.NSI_Client;

namespace AdminPanel.Models.Models.NSI_Product
{
	public class ProductCommentModel
	{
		public Guid id { get; set; }
		public int rating { get; set; }
		public int vendor_rating { get; set; }
		public int? delivery_rating { get; set; }
		public string text { get; set; }
		public string header { get; set; }
		public DateTime date { get; set; }
		public Guid clientid { get; set; }
		public ClientModel client { get; set; }
		public Guid productid { get; set; }
		public ProductModel product { get; set; }
	}
}
