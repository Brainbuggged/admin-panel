using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using AdminPanel.Models.Models.NSI_Client;
using AdminPanel.Models.Models.NSI_Order;
using AdminPanel.Models.Models.NSI_Product;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models.Models.NSI_Vendor
{
	[Index("number")]
	public class VendorModel
	{
		[Display(Name = "Идентификатор")]

		public Guid id { get; set; }
		[Display(Name = "Фото")]

		public string photo { get; set; }
		[Display(Name = "Номер")]

		public string number { get; set; }
		[Display(Name = "Имя")]

		public string name { get; set; }
		[Display(Name = "Фамилия")]

		public string surname { get; set; }
		[Display(Name = "Отчество")]

		public string patronymic { get; set; }
		[Display(Name = "Телефон")]

		public string phone { get; set; }
		[Display(Name = "Рабочий телефон")]

		public string work_phone { get; set; }
		[Display(Name = "ФизЛицо?")]

		public bool? is_fiz_face { get; set; }
		[Display(Name = "ЮрЛицо?")]

		public bool? is_ur_face { get; set; }
		[Display(Name = "Рейтинг")]

		public double rating { get; set; }
		[Display(Name = "Количество комментариев")]

		public int comment_count { get; set; }
		[Display(Name = "Обновлен ли ассортимент")]

		public bool is_assortment_updated { get; set; }
		[Display(Name = "Почта")]

		public string email { get; set; }
		[Display(Name = "Дата обновления ассортимента")]

		public DateTime last_assortiment_updated_date { get; set; }

		public List<ProductModel> products { get; set; }
		public List<FavouriteVendorModel> favourites { get; set; }
		public List<OrderModel> orders { get; set; }
		public List<VendorSocialModel> socials { get; set; }
	}
}
