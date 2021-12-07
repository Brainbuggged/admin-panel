using AdminPanel.Extensions;
using AdminPanel.Models;
using AdminPanel.Models.Models.NSI_Client;
using AdminPanel.Models.Models.NSI_Product;
using AdminPanel.Models.Models.NSI_Vendor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.DataAccessLayer { 

	public class Randomer
	{
		public List<jsoner> res { get; set; }
		public Randomer() => res = new List<jsoner>();
		public int makecount(int start, int end)
		{
			return new Random(Guid.NewGuid().GetHashCode() * new SettingsExtension().GetDateTimeNow().GetHashCode()).Next(start, end);
		}
		//public List<VendorModel> RandomVendors()
		//{
		//	var vendors = new List<VendorModel>();
		//	int num = 1;
		//	vendors.Add(new VendorModel
		//	{
		//		id = Guid.NewGuid(),
		//		number = num,
		//		rating = makecount(1, 10),
		//		phone = "88005553535",
		//		name = "ООО ХоккейАтлет",
		//		is_fiz_face = true,
		//		is_ur_face = false
		//	});
		//	num++;
		//	vendors.Add(new VendorModel
		//	{
		//		id = Guid.NewGuid(),
		//		number = num,
		//		rating = makecount(1, 10),
		//		phone = "88005553535",
		//		name = "ООО ХоккейКонтинент",
		//		is_fiz_face = true,
		//		is_ur_face = false
		//	});
		//	num++;
		//	vendors.Add(new VendorModel
		//	{
		//		id = Guid.NewGuid(),
		//		number = num,
		//		rating = makecount(1, 10),
		//		phone = "88005553535",
		//		name = "ООО ХоккейВиз",
		//		is_fiz_face = true,
		//		is_ur_face = false
		//	});
		//	num++;
		//	vendors.Add(new VendorModel
		//	{
		//		id = Guid.NewGuid(),
		//		number = num,
		//		rating = makecount(1, 10),
		//		phone = "88005553535",
		//		name = "ООО КонсалтХоккей",
		//		is_fiz_face = true,
		//		is_ur_face = false
		//	});
		//	num++;
		//	vendors.Add(new VendorModel
		//	{
		//		id = Guid.NewGuid(),
		//		number = num,
		//		rating = makecount(1, 10),
		//		phone = "88005553535",
		//		name = "ИП Тамила",
		//		is_fiz_face = true,
		//		is_ur_face = false
		//	});
		//	num++;
		//	vendors.Add(new VendorModel
		//	{
		//		id = Guid.NewGuid(),
		//		number = num,
		//		rating = makecount(1, 10),
		//		phone = "88005553535",
		//		name = "ОАО Хоккейто",
		//		is_fiz_face = true,
		//		is_ur_face = false
		//	});
		//	num++;
		//	vendors.Add(new VendorModel
		//	{
		//		id = Guid.NewGuid(),
		//		number = num,
		//		rating = makecount(1, 10),
		//		phone = "88005553535",
		//		name = "ОАО Хоккейинт",
		//		is_fiz_face = true,
		//		is_ur_face = false
		//	});
		//	num++;
		//	vendors.Add(new VendorModel
		//	{
		//		id = Guid.NewGuid(),
		//		number = num,
		//		rating = makecount(1, 10),
		//		phone = "88005553535",
		//		name = "ИП Леонилла",
		//		is_fiz_face = true,
		//		is_ur_face = false
		//	});
		//	return vendors;
		//}
		public List<ClientModel> RandomClients()
		{
			var clients = new List<ClientModel>();
			clients.Add(new ClientModel
			{
				id = Guid.NewGuid(),
				surname = "Орлов",
				name = "Аркадий",
				patronymic = "Александрович",
				login = "orlov",
				password = new EncrypterExtension().Encrypt("orlov"),
				role = RoleType.Client,
				vendorid = null,
				photo = "https://avatars.mds.yandex.net/get-dialogs/1676983/3edf79bb28bd3397abc8/orig",
				email = "email@email.ru"
			});
			clients.Last().number = new TranslitExtension().MakeName($"{clients.Last().surname} {clients.Last().name} {clients.Last().patronymic}");
			clients.Add(new ClientModel
			{
				id = Guid.NewGuid(),
				surname = "Новиков",
				name = "Матвей",
				patronymic = "Иванович",
				login = "novikov",
				password = new EncrypterExtension().Encrypt("novikov"),
				role = RoleType.Client,
				vendorid = null,
				photo = "https://avatars.mds.yandex.net/get-dialogs/1676983/3edf79bb28bd3397abc8/orig",
				email = "email@email.ru"
			});
			clients.Last().number = new TranslitExtension().MakeName($"{clients.Last().surname} {clients.Last().name} {clients.Last().patronymic}");
			clients.Add(new ClientModel
			{
				id = Guid.NewGuid(),
				surname = "Иванов",
				name = "Дмитрий",
				patronymic = "Павлович",
				login = "ivanov",
				password = new EncrypterExtension().Encrypt("ivanov"),
				role = RoleType.Client,
				vendorid = null,
				photo = "https://avatars.mds.yandex.net/get-dialogs/1676983/3edf79bb28bd3397abc8/orig",
				email = "email@email.ru"
			});
			clients.Last().number = new TranslitExtension().MakeName($"{clients.Last().surname} {clients.Last().name} {clients.Last().patronymic}");
			var vendorId = Guid.NewGuid();
			clients.Add(new ClientModel
			{
				id = Guid.NewGuid(),
				surname = "Зуев",
				name = "Кирилл",
				patronymic = "Артёмович",
				login = "zuev",
				password = new EncrypterExtension().Encrypt("zuev"),
				role = RoleType.Seller,
				photo = "https://avatars.mds.yandex.net/get-dialogs/1676983/3edf79bb28bd3397abc8/orig",
				email = "email@email.ru",
				vendor = new VendorModel
				{
					id = vendorId,
					rating = makecount(1, 10),
					phone = "88005553535",
					surname = "Зуев",
					name = "Кирилл",
					patronymic = "Артёмович",
					is_fiz_face = false,
					is_ur_face = true,
					photo = "https://image.flaticon.com/icons/png/512/206/206874.png",
					socials = RandomSocials(vendorId),
					is_assortment_updated = false,
					work_phone = "88005553535",
					email = "email@email.ru",
				}
			});
			clients.Last().number = new TranslitExtension().MakeName($"{clients.Last().surname} {clients.Last().name} {clients.Last().patronymic}");
			clients.Last().vendor.number = new TranslitExtension().MakeName($"{clients.Last().surname} {clients.Last().name} {clients.Last().patronymic}");
			vendorId = Guid.NewGuid();
			clients.Add(new ClientModel
			{
				id = Guid.NewGuid(),
				name = "Алексей",
				surname = "Сергеев",
				patronymic = "Сергеевич",
				login = "sergeev",
				password = new EncrypterExtension().Encrypt("sergeev"),
				role = RoleType.Seller,
				photo = "https://avatars.mds.yandex.net/get-dialogs/1676983/3edf79bb28bd3397abc8/orig",
				email = "email@email.ru",
				vendor = new VendorModel
				{
					id = vendorId,
					rating = makecount(1, 10),
					phone = "88005553535",
					name = "Алексей",
					surname = "Сергеев",
					patronymic = "Сергеевич",
					is_fiz_face = false,
					is_ur_face = true,
					photo = "https://image.flaticon.com/icons/png/512/206/206874.png",
					socials = RandomSocials(vendorId),
					is_assortment_updated = false,
					work_phone = "88005553535",
					email = "email@email.ru",
				}
			});
			clients.Last().number = new TranslitExtension().MakeName($"{clients.Last().surname} {clients.Last().name} {clients.Last().patronymic}");
			clients.Last().vendor.number = new TranslitExtension().MakeName($"{clients.Last().surname} {clients.Last().name} {clients.Last().patronymic}");
			vendorId = Guid.NewGuid();
			clients.Add(new ClientModel
			{
				id = Guid.NewGuid(),
				name = "Краснов",
				surname = "Владимир",
				patronymic = "Михайлович",
				login = "krasnov",
				password = new EncrypterExtension().Encrypt("krasnov"),
				role = RoleType.Seller,
				photo = "https://avatars.mds.yandex.net/get-dialogs/1676983/3edf79bb28bd3397abc8/orig",
				email = "email@email.ru",
				vendor = new VendorModel
				{
					id = vendorId,
					rating = makecount(1, 10),
					phone = "88005553535",
					name = "Краснов",
					surname = "Владимир",
					patronymic = "Михайлович",
					is_fiz_face = true,
					is_ur_face = false,
					photo = "https://image.flaticon.com/icons/png/512/206/206874.png",
					socials = RandomSocials(vendorId),
					is_assortment_updated = false,
					work_phone = "88005553535",
					email = "email@email.ru",
				}
			});
			clients.Last().number = new TranslitExtension().MakeName($"{clients.Last().surname} {clients.Last().name} {clients.Last().patronymic}");
			clients.Last().vendor.number = new TranslitExtension().MakeName($"{clients.Last().surname} {clients.Last().name} {clients.Last().patronymic}");
			vendorId = Guid.NewGuid();
			clients.Add(new ClientModel
			{
				id = Guid.NewGuid(),
				name = "Петрова",
				surname = "Алиса",
				patronymic = "Ивановна",
				login = "petrova",
				password = new EncrypterExtension().Encrypt("petrova"),
				role = RoleType.Seller,
				photo = "https://avatars.mds.yandex.net/get-dialogs/1676983/3edf79bb28bd3397abc8/orig",
				email = "email@email.ru",
				vendor = new VendorModel
				{
					id = vendorId,
					rating = makecount(1, 10),
					phone = "88005553535",
					name = "Петрова",
					surname = "Алиса",
					patronymic = "Ивановна",
					is_fiz_face = true,
					is_ur_face = false,
					photo = "https://image.flaticon.com/icons/png/512/206/206874.png",
					socials = RandomSocials(vendorId),
					is_assortment_updated = false,
					work_phone = "88005553535",
					email = "email@email.ru",
				}
			});
			clients.Last().number = new TranslitExtension().MakeName($"{clients.Last().surname} {clients.Last().name} {clients.Last().patronymic}");
			clients.Last().vendor.number = new TranslitExtension().MakeName($"{clients.Last().surname} {clients.Last().name} {clients.Last().patronymic}");
			return clients;
		}
		public List<ProductCommentModel> RandomComments(Guid productId, List<ClientModel> clients)
		{
			var comments = new List<ProductCommentModel>();
			comments.Add(new ProductCommentModel
			{
				id = Guid.NewGuid(),
				productid = productId,
				header = "Лучший товар",
				text = $"Лучший в своем роде",
				date = new SettingsExtension().GetDateTimeNow(),
				rating = makecount(4, 6),
				clientid = clients[makecount(0, 4)].id,
				delivery_rating = makecount(4, 6),
				vendor_rating = makecount(4, 6)
			});
			comments.Add(new ProductCommentModel
			{
				id = Guid.NewGuid(),
				productid = productId,
				header = "Самый дешовый из всiх",
				text = $"Очень дешевый и качественный",
				date = new SettingsExtension().GetDateTimeNow(),
				rating = makecount(4, 6),
				clientid = clients[makecount(0, 4)].id,
				delivery_rating = makecount(4, 6),
				vendor_rating = makecount(4, 6)
			});
			comments.Add(new ProductCommentModel
			{
				id = Guid.NewGuid(),
				productid = productId,
				header = "ОБМАН",
				text = $"Помойка, слишком дорого. Сломался на второй день",
				date = new SettingsExtension().GetDateTimeNow(),
				rating = makecount(1, 2),
				clientid = clients[makecount(0, 4)].id,
				delivery_rating = makecount(3, 5),
				vendor_rating = makecount(2, 4)
			});
			comments.Add(new ProductCommentModel
			{
				id = Guid.NewGuid(),
				productid = productId,
				header = "Нехороший продавец ***",
				text = $"Продавец обманул, вместо товара получил кусок шифера",
				date = new SettingsExtension().GetDateTimeNow(),
				rating = 1,
				clientid = clients[makecount(0, 4)].id,
				delivery_rating = 1,
				vendor_rating = 1
			});
			comments.Add(new ProductCommentModel
			{
				id = Guid.NewGuid(),
				productid = productId,
				header = "Детская мечта",
				text = $"С дестве мечтал о таком. Цена супер. Всем советую",
				date = new SettingsExtension().GetDateTimeNow(),
				rating = 5,
				clientid = clients[makecount(0, 4)].id,
				delivery_rating = 5,
				vendor_rating = 5
			});
			comments.Add(new ProductCommentModel
			{
				id = Guid.NewGuid(),
				productid = productId,
				header = "Подарок",
				text = $"Взял в подарок брату. Четвертый день нарадоваться не может",
				date = new SettingsExtension().GetDateTimeNow(),
				rating = makecount(4, 6),
				clientid = clients[makecount(0, 4)].id,
				delivery_rating = makecount(4, 6),
				vendor_rating = makecount(4, 6)
			});
			comments.Add(new ProductCommentModel
			{
				id = Guid.NewGuid(),
				productid = productId,
				header = "Подарок",
				text = $"Взял в подарок брату. Четвертый день нарадоваться не может",
				date = new SettingsExtension().GetDateTimeNow(),
				rating = makecount(2, 6),
				clientid = clients[makecount(0, 4)].id,
				delivery_rating = makecount(2, 6),
				vendor_rating = makecount(2, 6)
			});
			return comments.Take(makecount(2, 8)).ToList();
		}
		public List<VendorSocialModel> RandomSocials(Guid vendorId)
		{
			var socials = new List<VendorSocialModel>();
			var count = makecount(0, 7);
			for (int i = 0; i < count; i++)
			{
				var rnd = makecount(0, 7);
				switch (rnd)
				{
					case 0:
						if (socials.FirstOrDefault(item => item.type == SocialType.instagramm) == null)
						{
							socials.Add(new VendorSocialModel
							{
								id = Guid.NewGuid(),
								linq = "https://www.instagram.com/",
								type = SocialType.instagramm,
								vendorid = vendorId
							});
						}
						break;
					case 1:
						if (socials.FirstOrDefault(item => item.type == SocialType.odnoklassniki) == null)
						{
							socials.Add(new VendorSocialModel
							{
								id = Guid.NewGuid(),
								linq = "https://ok.ru/",
								type = SocialType.odnoklassniki,
								vendorid = vendorId
							});
						}
						break;
					case 2:
						if (socials.FirstOrDefault(item => item.type == SocialType.facebook) == null)
						{
							socials.Add(new VendorSocialModel
							{
								id = Guid.NewGuid(),
								linq = "https://www.facebook.com/",
								type = SocialType.facebook,
								vendorid = vendorId
							});
						}
						break;
					case 3:
						if (socials.FirstOrDefault(item => item.type == SocialType.vk) == null)
						{
							socials.Add(new VendorSocialModel
							{
								id = Guid.NewGuid(),
								linq = "https://vk.com/",
								type = SocialType.vk,
								vendorid = vendorId
							});
						}
						break;
					case 4:
						if (socials.FirstOrDefault(item => item.type == SocialType.youtube) == null)
						{
							socials.Add(new VendorSocialModel
							{
								id = Guid.NewGuid(),
								linq = "https://www.youtube.com/",
								type = SocialType.youtube,
								vendorid = vendorId
							});
						}
						break;
					case 5:
						if (socials.FirstOrDefault(item => item.type == SocialType.twitter) == null)
						{
							socials.Add(new VendorSocialModel
							{
								id = Guid.NewGuid(),
								linq = "https://twitter.com/",
								type = SocialType.twitter,
								vendorid = vendorId
							});
						}
						break;
					default:
						if (socials.FirstOrDefault(item => item.type == SocialType.tiktok) == null)
						{
							socials.Add(new VendorSocialModel
							{
								id = Guid.NewGuid(),
								linq = "https://www.tiktok.com/",
								type = SocialType.tiktok,
								vendorid = vendorId
							});
						}
						break;
				}
			}
			return socials;
		}
		public double makeprise(int val)
		{
			int valer = (new Random().Next() + val) % 60000;
			valer = new Random().Next(valer, 50000);
			return valer;
		}
		public ProductType prodtypeRandomer()
		{
			switch (makecount(0, 2))
			{
				case 1:
					return ProductType.BU;
				default:
					return ProductType.Noviy;
			}
		}
		public ProductStatus prodStatusRandomer()
		{
			switch (makecount(0, 15))
			{
				case 1:
					return ProductStatus.Vistavlen;
				case 2:
					return ProductStatus.Snyat;
				case 3:
					return ProductStatus.Podtverzhden;
				case 4:
					return ProductStatus.OjidaetProverki;
				case 5:
					return ProductStatus.Chernovik;
				case 6:
					return ProductStatus.Archive;
				case 7:
					return ProductStatus.udalen;
				case 8:
					return ProductStatus.zakonchilsya;
				default:
					return ProductStatus.Vistavlen;
			}
		}
		public bool prodBoolRandomer()
		{
			switch (makecount(0, 2))
			{
				case 1:
					return true;
				default:
					return false;
			}
		}
		public string prodCityRandomer(string propertyName)
		{
			switch (propertyName)
			{
				case "Город":
					var rnd = makecount(0, 5);
					switch (rnd)
					{
						case 0:
							return "Москва";
						case 2:
							return "Нара";
						case 3:
							return "Звенигород";
						case 4:
							return "Голицыно";
						default:
							return "Сергиев Посад";
					}
				default:
					return "";
			}
		}
		public string newPropertyRandomer(string categoryname, string propertyname)
		{
			try
			{
				var category = res.Single(cat => cat.en_name == categoryname);
				var property = category.parameters.Single(par => par.name == propertyname);
				var propertycount = property.values.Count();
				var rnd = makecount(0, propertycount);
				return property.values[rnd];
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}
	}
}
