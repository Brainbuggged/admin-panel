using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.ViewModels.Product.GetMyProducts.Response
{
	public class ResponseMyProduct
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
		public string product_status { get; set; }//product
		public int product_count { get; set; }
		public bool product_is_delivery_expected { get; set; }

		public string product_category { get; set; }//product_categories
		public string product_photo { get; set; }//product_photoes
	}
}
