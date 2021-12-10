using System;
using System.ComponentModel.DataAnnotations;
using AdminPanel.Models.Models.NSI_Product;

namespace AdminPanel.Models.Models.NSI_Order
{
	public class OrderProductModel
	{
		[Display(Name = "Идентификатор")]

		public Guid id { get; set; }
		[Display(Name = "Идентификатор продукта")]
		public string product_id { get; set; }
		[Display(Name = "Номер продукта")]

		public string product_number { get; set; }
		[Display(Name = "Дата публикации")]

		public DateTime publication_date { get; set; }
		[Display(Name = "Фото")]

		public string photo { get; set; }
		[Display(Name = "Название")]

		public string name { get; set; }
		[Display(Name = "Страна")]

		public string country { get; set; }
		[Display(Name = "Город")]

		public string city { get; set; }
		[Display(Name = "Название категории")]

		public string category_name { get; set; }
		[Display(Name = "Бренд")]

		public string brand { get; set; }
		[Display(Name = "Цена")]

		public double prise { get; set; }
		[Display(Name = "Количество")]

		public int count { get; set; }
		[Display(Name = "Общая стоимость")]

		public double total_prise { get; set; }

		public Guid orderid { get; set; }
		public OrderModel order { get; set; }

		public Guid categoryid { get; set; }
		public ProductCategoryModel category { get; set; }
	}
	public class OrderProductViewModel
	{

	}
}
