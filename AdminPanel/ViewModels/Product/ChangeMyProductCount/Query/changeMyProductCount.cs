using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.ViewModels.Product.ChangeMyProductCount.Query
{
	public class changeMyProductCount
	{
		public Guid productId { get; set; }
		public int productCount { get; set; }
	}
}
