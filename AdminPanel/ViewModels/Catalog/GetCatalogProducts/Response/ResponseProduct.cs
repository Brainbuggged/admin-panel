using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models.Catalog.GetCatalogProducts.Response
{
	public class ResponseProduct
	{
		public string productPublishDate { get; set; }//advertisement
		public string productName { get; set; }//product
		public string productPhoto { get; set; }
		public string productNumber { get; set; }
		public Guid productId { get; set; }//product
		public string productCity { get; set; }//product
		public string productCountry { get; set; }//product
		public int productYear { get; set; }//product
		public string productType { get; set; }//product
		public string productCategory { get; set; }//product.category
		public string productBrand { get; set; }//product
		public double productRating { get; set; }
		public double vendorRating { get; set; }//vendor
		public double productPrise { get; set; }//product
		public bool is_in_cart { get; set; }
		public bool is_in_favourite { get; set; }
	}
}
