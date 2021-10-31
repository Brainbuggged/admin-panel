using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.ViewModels.Order.ChangeOrderStatus.Query
{
	public class changeOrderStatus
	{
		public Guid orderId { get; set; }
		public string status { get; set; }
		public string role { get; set; }
	}
}
