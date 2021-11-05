using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.ViewModels.Product.CreateProduct.Query
{
	public class createProduct
	{
		public string product_name { get; set; }
		public string product_category { get; set; }
		public string product_type { get; set; }
		public List<createProductProperty> product_properties { get; set; }
		public string product_brand { get; set; }
		public double product_prise { get; set; }
		public int product_release_year { get; set; }
		public string product_description { get; set; }
		public List<createProductPhoto> product_photoes { get; set; }
		public int product_amount { get; set; }
		public string product_address { get; set; }
		public string product_country { get; set; }
		public string product_city { get; set; }
		public double product_x { get; set; }
		public double product_y { get; set; }
		public bool product_is_pickuped { get; set; }
		public bool product_is_delivered { get; set; }
	}
}
