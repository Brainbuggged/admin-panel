using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models.Catalog.GetCatalogProducts.Query
{
	public class GetProductsWithFilters
	{
		public string name { get; set; }
		public List<string> values { get; set; }
		public double minValue { get; set; }
		public double maxValue { get; set; }
	}
}
