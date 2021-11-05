using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models.Catalog.GetCategories.Response
{
	public class ResponseCategory
	{
		public bool is_last { get; set; }
		public string ru_name { get; set; }
		public string en_name { get; set; }
		public string photo { get; set; }
	}
}
