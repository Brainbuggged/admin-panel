using System;
using AdminPanel.Models.Models.NSI_Vendor;

namespace AdminPanel.Models.Models.NSI_Client
{
	public class FavouriteVendorModel
	{
		public Guid id { get; set; }
		public bool? is_notify_required { get; set; }
		public Guid vendorid { get; set; }
		public VendorModel vendor { get; set; }
		public Guid clientid { get; set; }
		public ClientModel client { get; set; }
	}
}
