using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using AdminPanel.Models.Models.NSI_Client;
using AdminPanel.Models.Models.NSI_Vendor;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models.Models.NSI_Order
{
	[Index("number")]
	public class OrderModel
	{
		[Display(Name = "Идентификатор")]

		public Guid id { get; set; }

		[Display(Name = "Номер")]

		public string number { get; set; }

		[Display(Name = "Цена товара")]

		public double positions_prise { get; set; }
		[Display(Name = "Стоимость доставки")]

		public double delivery_prise { get; set; }
		[Display(Name = "Общая выручка")]

		public double total_prise { get; set; }
		[Display(Name = "Количество проданных товаров")]
		public int position_count { get; set; }

		public double x { get; set; }
		public double y { get; set; }
		public string address { get; set; }

		[Display(Name = "Дата и время заказа")]

		public DateTime date { get; set; }
		[Display(Name = "Статус заказа")]

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
