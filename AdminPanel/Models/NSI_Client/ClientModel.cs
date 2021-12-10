using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AdminPanel.Models.Models.NSI_Order;
using AdminPanel.Models.Models.NSI_Product;
using AdminPanel.Models.Models.NSI_Vendor;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Models.Models.NSI_Client
{
	[Index("number", "login")]
	public class ClientModel
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
		[Display(Name = "Баланс")]

		public double balance { get; set; }
		[Display(Name = "Номер банковской карты")]

		public string card_number { get; set; }
		[Display(Name = "Дата создания корзины")]

		public DateTime card_date { get; set; }
		[Display(Name = "CVV")]

		public string cvv { get; set; }
		[Display(Name = "Логин")]

		public string login { get; set; }
		[Display(Name = "Пароль")]

		public string password { get; set; }
		[Display(Name = "Почта")]

		public string email { get; set; }

		[Display(Name = "Роль")]
		public RoleType role { get; set; }
				
		[Display(Name = "Продавец")]
		public Guid? vendorid { get; set; }
		public VendorModel vendor { get; set; }

		public List<CartItemModel> cart { get; set; }
		public List<FavouriteProductModel> favourite_products { get; set; }
		public List<FavouriteVendorModel> favourite_vendors { get; set; }
		public List<OrderModel> orders { get; set; }
		public List<ProductCommentModel> comments { get; set; }
	}
}
