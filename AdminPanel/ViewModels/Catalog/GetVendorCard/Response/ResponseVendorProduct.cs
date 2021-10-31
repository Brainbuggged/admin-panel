using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models.Catalog.GetVendorCard.Response
{
	public class ResponseVendorProduct
	{
		public Guid product_id { get; set; }
		public string product_publish_date { get; set; }//product
		public string product_name { get; set; }//product
		public string product_number { get; set; }//product
		public string product_city { get; set; }//product
		public string product_country { get; set; }//product
		public string product_type { get; set; }//product
		public string product_brand { get; set; }//product
		public double product_rating { get; set; }
		public double product_prise { get; set; }//product

		public string product_category { get; set; }//product_categories
		public bool is_in_cart { get; set; }//inner join
		public bool is_in_favourite { get; set; }//inner join
		public string product_photo { get; set; }//product_photoes
	}
}
