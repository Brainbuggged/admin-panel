using System;
using System.Collections.Generic;
using AdminPanel.Models.Models.NSI_Order;
using AdminPanel.Models.Models.NSI_Product;
using AdminPanel.Models.Models.NSI_Vendor;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Models.Models.NSI_Client
{
	[Index("number", "login")]
	public class ClientModel
	{
		public Guid id { get; set; }
		public string photo { get; set; }
		public string number { get; set; }
		public string name { get; set; }
		public string surname { get; set; }
		public string patronymic { get; set; }
		public string phone { get; set; }
		public double balance { get; set; }
		public string card_number { get; set; }
		public DateTime card_date { get; set; }
		public string cvv { get; set; }
		public string login { get; set; }
		public string password { get; set; }
		public string email { get; set; }

		public RoleType role { get; set; }

		public Guid? vendorid { get; set; }
		public VendorModel vendor { get; set; }

		public List<CartItemModel> cart { get; set; }
		public List<FavouriteProductModel> favourite_products { get; set; }
		public List<FavouriteVendorModel> favourite_vendors { get; set; }
		public List<OrderModel> orders { get; set; }
		public List<ProductCommentModel> comments { get; set; }
	}
}
