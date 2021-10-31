using Microsoft.EntityFrameworkCore;
using System;

namespace AdminPanel.Models.Models.NSI_Vendor
{
	[Index("number")]
	public class VendorDraftRequestModel
	{
		public Guid id { get; set; }
		public int number { get; set; }
		public DateTime request_date { get; set; }
		public DateTime? closed_date { get; set; }
		public string message { get; set; }
		public string category { get; set; }
		public Guid vendorid { get; set; }
		public VendorModel Vendor { get; set; }
	}
}
