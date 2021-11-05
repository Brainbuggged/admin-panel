using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.ViewModels.Order.GetClientOrders.Response
{
	public class ResponseClientSubOrder
	{
		public int position_count { get; set; }
		public string vendor_name { get; set; }
		public string order_status { get; set; }
	}
}
