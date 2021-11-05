using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using AdminPanel.Models.Models.NSI_Client;
using AdminPanel.Models.Models.NSI_Vendor;

namespace AdminPanel.Models.Models.NSI_Order
{
	[Index("number")]
	public class OrderModel
	{
		public Guid id { get; set; }

		public string number { get; set; }

		public double positions_prise { get; set; }
		public double delivery_prise { get; set; }
		public double total_prise { get; set; }

		public int position_count { get; set; }

		public double x { get; set; }
		public double y { get; set; }
		public string address { get; set; }

		public DateTime date { get; set; }
		public OrderStatus status { get; set; }
		public DeliveryType delivery_type { get; set; }
		public PaymentType payment_type { get; set; }

		public Guid vendorid { get; set; }
		public VendorModel vendor { get; set; }

		public Guid clientid { get; set; }
		public ClientModel client { get; set; }

		public List<OrderProductModel> products { get; set; }
		public List<OrderStatusChangeModel> status_history { get; set; }
		public List<ConflictMessageModel> conflict_messages { get; set; }
	}
}
