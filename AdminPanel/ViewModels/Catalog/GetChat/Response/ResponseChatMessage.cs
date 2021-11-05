using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.ViewModels.Catalog.GetChat.Response
{
	public class ResponseChatMessage
	{
		public string message_text { get; set; }
		public string message_photo { get; set; }
		public string message_role { get; set; }
		public DateTime message_date { get; set; }
		public bool message_is_read { get; set; }
	}
}
