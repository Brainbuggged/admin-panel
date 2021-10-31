using AdminPanel.Models.Catalog.GetCatalogProducts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models.Catalog.GetVendorCard.Response
{
	public class ResponseVendorCard
	{
		public ResponseVendorInfo vendorInfo { get; set; }
		public List<ResponseVendorComment> vendorComments { get; set; }
		public List<ResponseVendorProduct> vendorProducts { get; set; }
		public List<ResponseSocialMedia> vendorSocials { get; set; }
	}
}
