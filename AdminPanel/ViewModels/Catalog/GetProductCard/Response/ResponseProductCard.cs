using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models.Catalog.GetProductCard.Response
{
	public class ResponseProductCard
	{
		public ResponseProductInfo productInfo { get; set; }
		public ResponseProductVendor vendorInfo { get; set; }
	}
}
