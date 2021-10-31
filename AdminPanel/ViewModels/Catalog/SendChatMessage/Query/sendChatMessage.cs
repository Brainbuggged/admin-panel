using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.ViewModels.Catalog.SendChatMessage.Query
{
	public class sendChatMessage
	{
		public string message_text { get; set; }
		public string sender_role { get; set; }
		public string reciever_number { get; set; }
	}
}
