using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.ViewModels.Order.CreateOrder.Query
{
	public class createOrder
	{
		public int deliveryType { get; set; }
		public string deliveryAddress { get; set; }
		public double x { get; set; }
		public double y { get; set; }
		public int paymentType { get; set; }
	}
}
