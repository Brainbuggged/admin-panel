using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.ViewModels.Order.GetOrderCard.Response
{
	public class ResponseOrderProduct
	{
		public DateTime product_publication_date { get; set; }
		public string product_number { get; set; }
		public string product_photo { get; set; }
		public string product_name { get; set; }
		public string product_country { get; set; }
		public string product_city { get; set; }
		public string product_category { get; set; }
		public string product_brand { get; set; }
		public double product_prise { get; set; }
		public int product_count { get; set; }
		public double product_total_cost { get; set; }
	}
}
