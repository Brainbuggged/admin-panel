using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AdminPanel.Models.Models.NSI_Client;
using AdminPanel.Models.Models.NSI_Vendor;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Models.Models.NSI_Product
{
	[Index("number")]
	public class ProductModel
	{
		[Display(Name = "Идентификатор")]

		public Guid id { get; set; }
		[Display(Name = "Номер")]

		public string number { get; set; }
		[Display(Name = "Дата размещения")]

		public DateTime publication_date { get; set; }
		[Display(Name = "Наименование")]

		public string name { get; set; }
		[Display(Name = "Страна")]

		public string country { get; set; }
		[Display(Name = "Город")]

		public string city { get; set; }
		[Display(Name = "Год выпуска")]

		public int release_year { get; set; }
		[Display(Name = "Тип")]

		public ProductType type { get; set; }
		[Display(Name = "Бренд")]

		public string brand { get; set; }
		[Display(Name = "Цена")]

		public double prise { get; set; }
		[Display(Name = "Количество")]

		public int count { get; set; }
		[Display(Name = "Ожидается ли доставка")]

		public bool is_delivery_expected { get; set; }
		[Display(Name = "Описание")]

		public string description { get; set; }
		[Display(Name = "Адрес")]

		public string address { get; set; }
		[Display(Name = "Рейтинг")]

		public double rating { get; set; }
		[Display(Name = "Статус")]

		public ProductStatus status { get; set; }
		[Display(Name = "Самовывоз")]

		public bool is_pickuped { get; set; }
		[Display(Name = "Доставка")]

		public bool is_delivered { get; set; }
		[Display(Name = "X")]
		public double x { get; set; }
		[Display(Name = "Y")]

		public double y { get; set; }
		[Display(Name = "Проверен?")]

		public bool is_checked { get; set; }
		[Display(Name = "Время последнего обновления статуса")]

		public DateTime last_status_update { get; set; }

		public Guid categoryid { get; set; }
		public ProductCategoryModel category { get; set; }

		public Guid vendorid { get; set; }
		public VendorModel vendor { get; set; }

		public List<ProductPropertyModel> properties { get; set; }
		public List<ProductPhotoModel> photoes { get; set; }
		public List<CartItemModel> carts { get; set; }
		public List<FavouriteProductModel> favourites { get; set; }
		public List<ProductCommentModel> comments { get; set; }
	}
}
