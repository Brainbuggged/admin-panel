using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.ViewModels.Order.GetOrderCard.Response
{
	public class ResponseOrderCard
	{
		public Guid order_id { get; set; }
		public string order_number { get; set; }
		public DateTime order_date { get; set; }
		public List<ResponseOrderStatus> order_statuses { get; set; }
		public string order_delivery_type { get; set; }
		public string order_address { get; set; }
		public List<ResponseOrderProduct> order_products { get; set; }
		public string order_role { get; set; }
		public string order_status { get; set; }
	}
}
