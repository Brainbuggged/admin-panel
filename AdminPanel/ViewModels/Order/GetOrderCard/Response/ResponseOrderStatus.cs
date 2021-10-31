using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.ViewModels.Order.GetOrderCard.Response
{
	public class ResponseOrderStatus
	{
		public DateTime change_date { get; set; }
		public string order_old_status { get; set; }
		public string order_new_status { get; set; }
	}
}
