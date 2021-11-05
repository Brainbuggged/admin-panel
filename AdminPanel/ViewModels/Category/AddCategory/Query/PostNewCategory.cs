using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models.Category.AddCategory.Query
{
	public class PostNewCategory
	{
		public string categoryRuName { get; set; }
		public string parentCategoryRuName { get; set; }
		public List<string> parameters { get; set; }
	}
}
