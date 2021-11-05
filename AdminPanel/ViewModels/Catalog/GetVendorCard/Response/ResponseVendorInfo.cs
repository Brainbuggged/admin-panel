using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models.Catalog.GetVendorCard.Response
{
	public class ResponseVendorInfo
	{
		public string vendor_fio { get; set; }
		public Guid vendor_id { get; set; }
		public bool is_fiz_face { get; set; }
		public bool is_ur_face { get; set; }
		public string vendor_phone { get; set; }
		public double vendor_rating { get; set; }
		public int comment_count { get; set; }
		public bool is_in_favourite { get; set; }
	}
}
