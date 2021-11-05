using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models.Category.AddCategoryParameterValues
{
	public class addCategoryParameterValues
	{
		public string categoryName { get; set; }
		public string parameterName { get; set; }
		public List<string> values { get; set; }
	}
}
