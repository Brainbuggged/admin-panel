using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models.Catalog.GetProductCard.Response
{
	public class ResponseProductVendor
	{
		public string vendor_number { get; set; }
		public Guid vendor_id { get; set; }
		public string vendor_fio { get; set; }
		public string vendor_phone { get; set; }
		public bool is_fiz_face { get; set; }
		public bool is_ur_face { get; set; }
		public double vendor_rating { get; set; }
	}
}
