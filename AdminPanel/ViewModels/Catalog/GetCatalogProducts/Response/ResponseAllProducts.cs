using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models.Catalog.GetCatalogProducts.Response
{
	public class ResponseAllProducts
	{
		public int pageCount { get; set; }//db
		public int productCount { get; set; }//db
		public double minPrise { get; set; }
		public double maxPrise { get; set; }
		public int minYear { get; set; }
		public int maxYear { get; set; }
		public List<ResponseProduct> products { get; set; }
	}
}
