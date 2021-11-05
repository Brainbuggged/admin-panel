using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models.Catalog.GetProductCard.Response
{
	public class ResponseProductComment
	{
		public string client_photo { get; set; }
		public string client_name { get; set; }
		public int comment_rating { get; set; }
		public string comment_header { get; set; }
		public string comment_text { get; set; }
	}
}
