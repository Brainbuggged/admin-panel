using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models.Catalog.GetProductCard.Response
{
	public class ResponseProductInfo
	{
		public Guid productId { get; set; }
		public string productName { get; set; }//products
		public string productType { get; set; }//products
		public string productCategory { get; set; }//products
		public string productBrand { get; set; }//products
		public double productRating { get; set; }//products
		public double productPrise { get; set; }//products
		public int productCount { get; set; }//products
		public bool isDeliveryExpected { get; set; }//products
		public string productDescription { get; set; }//products
		public string productAddress { get; set; }//products
		public double X { get; set; }//products
		public double Y { get; set; }//products
		public string productPublicationDate { get; set; }//products

		public List<ResponseProductProperty> productProperties { get; set; }
		public List<ResponseProductPhoto> productPhotoes { get; set; }
		public List<ResponseProductComment> productComments { get; set; }

		public string productSizes { get; set; }
		public bool is_in_cart { get; set; }
		public bool is_in_favourite { get; set; }
	}
}
