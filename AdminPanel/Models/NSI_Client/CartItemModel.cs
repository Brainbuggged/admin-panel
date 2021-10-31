using System;
using AdminPanel.Models.Models.NSI_Product;

namespace AdminPanel.Models.Models.NSI_Client
{
	//[Index("adding_date")]
	public class CartItemModel
	{
		public Guid id { get; set; }
		public int count { get; set; }
		public DateTime adding_date { get; set; }
		public Guid productid { get; set; }
		public ProductModel product { get; set; }
		public Guid clientid { get; set; }
		public ClientModel client { get; set; }
	}
}
