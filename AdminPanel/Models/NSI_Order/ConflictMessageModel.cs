using System;

namespace AdminPanel.Models.Models.NSI_Order
{
	public class ConflictMessageModel
	{
		public Guid id { get; set; }
		public string message { get; set; }
		public RoleType sender { get; set; }
		public DateTime date { get; set; }
		public Guid orderid { get; set; }
		public OrderModel order { get; set; }
	}
}
