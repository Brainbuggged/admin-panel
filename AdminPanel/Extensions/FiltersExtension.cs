using AdminPanel.Models.Catalog.GetCatalogProducts.Query;
using AdminPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Extensions
{
	public class FiltersExtension
	{
		public CreatedFilters CreateFilters(string productCategory, string searchBody, int productCount, int pageNumber, int sortingType, bool bestRatingFirst, bool productInStock, int productType, string productCity, List<GetProductsWithFilters> filters)
		{
			var returner = new CreatedFilters();
			//////////////////////
			returner.Category = " product_categories pc where pc.en_name = " + '\u0027' + productCategory + '\u0027' + " and pr.categoryid = pc.id";
			//////////////////////
			returner.Search = " lower(pr.name) like lower(" + '\u0027' + $"%{searchBody}%" + '\u0027' + ")";
			//////////////////////
			if (productCity != "")
				returner.City = " lower(pr.city) = lower(" + '\u0027' + productCity + '\u0027' + ")";
			//////////////////////
			returner.Count = " limit " + '\u0027' + productCount + '\u0027' + " offset " + '\u0027' + (pageNumber - 1) * productCount + '\u0027';
			//////////////////////
			if (bestRatingFirst == true)
			{
				returner.Sorting += " rating desc, ";
			}
			//////////////////////
			switch (sortingType)
			{
				case 1: //По дате размещения по убыванию
					returner.Sorting += " publication_date desc";
					break;
				case 2://По дате размещения по возрастанию
					returner.Sorting += " publication_date asc";
					break;
				case 3://По стоимости по убыванию
					returner.Sorting += " prise desc";
					break;
				case 4://По стоимости по возрастанию
					returner.Sorting += " prise asc";
					break;
				default://По названию в алфавитном порядке
					returner.Sorting += " name asc";
					break;
			}
			//////////////////////
			if (productInStock == true)
				returner.Amount = " pr.count > " + '\u0027' + 0 + '\u0027';
			//////////////////////
			switch (productType)
			{
				case 1: //Новый
					returner.Type += " pr.type = " + '\u0027' + 0 + '\u0027';
					break;
				case 2://б/у
					returner.Type += " pr.type = " + '\u0027' + 1 + '\u0027';
					break;
				default://По умолчанию
					returner.Type += "";
					break;
			}
			//////////////////////
			var priseFilter = filters.FirstOrDefault(item => item.name == "Цена");
			if (priseFilter != null)
			{
				returner.Prise = " pr.prise >= " + '\u0027' + priseFilter.minValue + '\u0027' + " and pr.prise <= " + '\u0027' + priseFilter.maxValue + '\u0027';
				filters.Remove(priseFilter);
			}
			//////////////////////
			var yearFilter = filters.FirstOrDefault(item => item.name == "Год выпуска");
			if (yearFilter != null)
			{
				returner.Year = " pr.release_year >= " + '\u0027' + yearFilter.minValue + '\u0027' + " and pr.release_year <= " + '\u0027' + yearFilter.maxValue + '\u0027';
				filters.Remove(yearFilter);
			}
			//////////////////////
			var brandFilter = filters.FirstOrDefault(item => item.name == "Бренд");
			if (brandFilter != null)
			{
				var brands = "";
				foreach (var item in brandFilter.values)
					brands += '\u0027' + item + '\u0027' + ", ";
				returner.Brand += " brand in (" + brands.Substring(0, brands.Length - 2) + ") ";
				filters.Remove(brandFilter);
			}
			//////////////////////
			for (int i = 0; i < filters.Count; i++)
			{
				var values = "";
				foreach (var val in filters[i].values)
				{
					values += '\u0027' + val + '\u0027' + ", ";
				}
				if (i > 0)
					returner.Properties += " join (";
				returner.Properties +=
					"(select pr" + (i + 1) + ".publication_date , "
					+ "pr" + (i + 1) + ".name , "
					+ "pr" + (i + 1) + ".number, "
					+ "pr" + (i + 1) + ".city, "
					+ "pr" + (i + 1) + ".country, "
					+ "pr" + (i + 1) + ".release_year, "
					+ "pr" + (i + 1) + ".type, "
					+ "pr" + (i + 1) + ".brand, "
					+ "pr" + (i + 1) + ".rating, "
					+ "pr" + (i + 1) + ".categoryid, "
					+ "pr" + (i + 1) + ".id, "
					+ "pr" + (i + 1) + ".vendorid, "
					+ "pr" + (i + 1) + ".count, "
					+ "pr" + (i + 1) + ".prise"
					+ " from products pr" + (i + 1)
					+ " join product_properties prpr on pr" + (i + 1) + ".id = prpr.productid"
 					+ " where prpr.name = " + '\u0027' + filters[i].name + '\u0027'
					+ " and prpr.value in (" + values.Substring(0, values.Length - 2) + $") and pr{i + 1}.status in ({new SettingsExtension().AvailableProductStatuses()}))) pr" + (i + 1);
				if (i > 0)
					returner.Properties += " on pr1.id = pr" + (i + 1) + ".id";
			}
			//////////////////////

			return returner;
		}
	}

	public class CreatedFilters
	{
		public string Category { get; set; } = "";//
		public string Search { get; set; } = "";//
		public string Count { get; set; } = " limit " + '\u0027' + 12 + '\u0027' + " offset " + '\u0027' + 0 + '\u0027';//
		public string Sorting { get; set; } = " order by ";//
		public string Amount { get; set; } = " pr.count > " + '\u0027' + -1 + '\u0027';//

		public string Type { get; set; } = "";//

		public string Prise { get; set; } = "";//
		public string Year { get; set; } = "";//
		public string Brand { get; set; } = "";//
		public string Properties { get; set; } = "";//
		public string City { get; set; } = "";

	}

	public class ProductForReturn
	{
		public DateTime publication_date { get; set; }//advertisement
		public string name { get; set; }//product
		public string number { get; set; }//product
		public string city { get; set; }//product
		public string country { get; set; }//product
		public int release_year { get; set; }//product
		public ProductType type { get; set; }//product
		public string ru_name { get; set; }//product.category
		public string brand { get; set; }//product
		public double rating { get; set; }
		public double prise { get; set; }//product
		public bool is_in_cart { get; set; }
		public bool is_in_favourite { get; set; }
		public Guid id { get; set; }
		public Guid vendorid { get; set; }
	}
	public class ProductForOrder
	{
		public Guid id { get; set; }
		public string number { get; set; }//product
		public DateTime publication_date { get; set; }//advertisement
		public string photo { get; set; }//product
		public string name { get; set; }//product
		public string country { get; set; }//product
		public string city { get; set; }//product
		public string category_name { get; set; }//product.category
		public string brand { get; set; }//product
		public double prise { get; set; }
		public int count { get; set; }//product
		public Guid categoryid { get; set; }
		public int amount { get; set; }//product
		public Guid vendorid { get; set; }
	}
	public class OrderForGrouping
	{
		public DateTime date { get; set; }
		public Guid id { get; set; }
		public string number { get; set; }
		public int position_count { get; set; }
		public double total_prise { get; set; }
		public OrderStatus status { get; set; }
		public string vendor_name { get; set; }

	}
	public class OrderStatusForChange
	{
		public OrderStatus newStatus { get; set; }
		public OrderStatus oldStatus { get; set; }
		public RoleType roleType { get; set; }
		public DeliveryType deliveryType { get; set; }
	}
	public class ZeroProductToEmail
	{
		public string product_name { get; set; }
		public int product_number { get; set; }
		public string vendor_fio { get; set; }
		public string vendor_email { get; set; }
	}
}
