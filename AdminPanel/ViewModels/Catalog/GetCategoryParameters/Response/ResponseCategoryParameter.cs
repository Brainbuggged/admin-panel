using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models.Catalog.GetCategoryParameters.Response
{
	public class ResponseCategoryParameter
	{
		public string name { get; set; }
		public List<string> values { get; set; }
	}
}
