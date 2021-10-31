using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.ViewModels.Order.GetVendorOrders.Response
{
	public class ResponseVendorOrder
	{
		public Guid order_id { get; set; }
		public string order_number { get; set; }
		public DateTime order_date { get; set; }
		public string client_name { get; set; }
		public int order_position_count { get; set; }
		public double order_total_prise { get; set; }
		public string order_status { get; set; }
	}
}
