using System;
using AdminPanel.Models.Models.NSI_Vendor;

namespace AdminPanel.Models.Models.NSI_Client
{
	public class ChatMessageModel
	{
		public Guid id { get; set; }
		public string text { get; set; }
		public DateTime date { get; set; }
		public RoleType reciever { get; set; }
		public bool is_read { get; set; }
		public Guid clientid { get; set; }
		public ClientModel client { get; set; }
		public Guid vendorid { get; set; }
		public VendorModel vendor { get; set; }
	}
}
