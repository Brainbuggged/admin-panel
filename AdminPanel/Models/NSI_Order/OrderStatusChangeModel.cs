using System;

namespace AdminPanel.Models.Models.NSI_Order
{
	public class OrderStatusChangeModel
	{
		public Guid id { get; set; }
		public OrderStatus old_status { get; set; }
		public OrderStatus new_status { get; set; }
		public DateTime date { get; set; }
		public DateTime time { get; set; }
		public Guid orderid { get; set; }
		public OrderModel order { get; set; }
	}
}
