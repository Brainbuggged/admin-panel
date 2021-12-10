using System;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models.Models.NSI_Order
{
	public class OrderStatusChangeModel
	{
		public Guid id { get; set; }
		[Display(Name = "Старый статус")]

		public OrderStatus old_status { get; set; }
		[Display(Name = "Новый статус")]

		public OrderStatus new_status { get; set; }
		[Display(Name = "Дата время")]

		public DateTime date { get; set; }
		public DateTime time { get; set; }
		public Guid orderid { get; set; }
		public OrderModel order { get; set; }
	}

	public class OrderStatusChangeModelView
	{
		[Display(Name = "Старый статус")]

		public string old_status { get; set; }
		[Display(Name = "Новый статус")]

		public string new_status { get; set; }
		[Display(Name = "Дата время")]

		public DateTime date { get; set; }
	}
}
