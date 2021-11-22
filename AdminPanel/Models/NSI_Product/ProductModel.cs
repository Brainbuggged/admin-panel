using System;
using System.Collections.Generic;
using AdminPanel.Models.Models.NSI_Client;
using AdminPanel.Models.Models.NSI_Vendor;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Models.Models.NSI_Product
{
	[Index("number")]
	public class ProductModel
	{
		public Guid id { get; set; }
		public string number { get; set; }
		public DateTime publication_date { get; set; }
		public string name { get; set; }
		public string country { get; set; }
		public string city { get; set; }
		public int release_year { get; set; }
		public ProductType type { get; set; }
		public string brand { get; set; }
		public double prise { get; set; }
		public int count { get; set; }
		public bool is_delivery_expected { get; set; }
		public string description { get; set; }
		public string address { get; set; }
		public double rating { get; set; }
		public ProductStatus status { get; set; }
		public bool is_pickuped { get; set; }
		public bool is_delivered { get; set; }
		public double x { get; set; }
		public double y { get; set; }
		public bool is_checked { get; set; }
		public DateTime last_status_update { get; set; }

		public Guid categoryid { get; set; }
		public ProductCategoryModel category { get; set; }

		public Guid vendorid { get; set; }
		public VendorModel vendor { get; set; }

		public List<ProductPropertyModel> properties { get; set; }
		public List<ProductPhotoModel> photoes { get; set; }
		public List<CartItemModel> carts { get; set; }
		public List<FavouriteProductModel> favourites { get; set; }
		public List<ProductCommentModel> comments { get; set; }
	}
}
