using System;

namespace AdminPanel.Models.Models.NSI_Vendor
{
	public class VendorSocialModel
	{
		public Guid id { get; set; }
		public SocialType type { get; set; }
		public string linq { get; set; }
		public Guid vendorid { get; set; }
		public VendorModel vendor { get; set; }

	}
}
