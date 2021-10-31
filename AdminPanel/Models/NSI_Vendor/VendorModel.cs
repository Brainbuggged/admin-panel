using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using AdminPanel.Models.Models.NSI_Client;
using AdminPanel.Models.Models.NSI_Order;
using AdminPanel.Models.Models.NSI_Product;

namespace AdminPanel.Models.Models.NSI_Vendor
{
	[Index("number")]
	public class VendorModel
	{
		public Guid id { get; set; }
		public string photo { get; set; }
		public string number { get; set; }
		public string name { get; set; }
		public string surname { get; set; }
		public string patronymic { get; set; }
		public string phone { get; set; }
		public string work_phone { get; set; }
		public bool? is_fiz_face { get; set; }
		public bool? is_ur_face { get; set; }
		public double rating { get; set; }
		public int comment_count { get; set; }
		public bool is_assortment_updated { get; set; }
		public string email { get; set; }
		public DateTime last_assortiment_updated_date { get; set; }

		public List<ProductModel> products { get; set; }
		public List<FavouriteVendorModel> favourites { get; set; }
		public List<OrderModel> orders { get; set; }
		public List<VendorSocialModel> socials { get; set; }
	}
}
