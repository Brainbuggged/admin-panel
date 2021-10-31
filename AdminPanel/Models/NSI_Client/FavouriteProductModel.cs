using System;
using AdminPanel.Models.Models.NSI_Product;

namespace AdminPanel.Models.Models.NSI_Client
{
	public class FavouriteProductModel
	{
		public Guid id { get; set; }
		public bool is_notify_required { get; set; }
		public Guid productid { get; set; }
		public ProductModel product { get; set; }
		public Guid clientid { get; set; }
		public ClientModel client { get; set; }
	}
}
