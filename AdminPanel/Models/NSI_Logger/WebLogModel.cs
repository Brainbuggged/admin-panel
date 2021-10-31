using Microsoft.EntityFrameworkCore;
using System;

namespace AdminPanel.Models.Models.NSI_Logger
{
	[Index("method", "remote_ip_address")]
	public class WebLogModel
	{
		public Guid id { get; set; }
		public double content_length { get; set; }
		//public string content_type { get; set; }
		public string path { get; set; }
		public string query_string { get; set; }
		//public string url { get; set; }
		//public string host { get; set; }
		public string body { get; set; }
		public string method { get; set; }
		public DateTime date { get; set; }

		//public LogType type { get; set; }
		public int status_code { get; set; }
		public string remote_ip_address { get; set; }
		public string exemption { get; set; }
	}
}
