using System.ComponentModel;

namespace AdminPanel.Models
{
	public static class Enums
	{
		private static string GetString(object environment)
		{
			// get the field 
			var field = environment.GetType().GetField(environment.ToString());
			var customAttributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

			if (customAttributes.Length > 0)
				return (customAttributes[0] as DescriptionAttribute).Description;
			else
				return environment.ToString();
		}
		public static string GetText(this RoleType environment) => GetString(environment);
		public static string GetText(this LogType environment) => GetString(environment);
		public static string GetText(this OrderStatus environment) => GetString(environment);
		public static string GetText(this DeliveryType environment) => GetString(environment);
		public static string GetText(this PaymentType environment) => GetString(environment);
		public static string GetText(this ProductType environment) => GetString(environment);
		public static string GetText(this ProductStatus environment) => GetString(environment);
		public static string GetText(this SocialType environment) => GetString(environment);
	}
	public enum RoleType
	{
		[Description("Покупатель")]
		Client = 0,
		[Description("Продавец")]
		Seller = 1,
		[Description("Администратор")]
		Admin = 2,
		[Description("Заморожен")]
		Blocked = 3
	}
	public enum LogType
	{
		[Description("Request")]
		Request = 0,
		[Description("Response")]
		Response = 1,
		[Description("Examption")]
		Exemption = 2
	}
	public enum OrderStatus
	{
		[Description("В обработке")]
		vObrabotke = 0,
		[Description("Ожидается отмена")]
		ojidaetsyaOtmena = 1,
		[Description("Ожидается отправка")]
		ojidaetsyaOtpravka = 2,
		[Description("В пути")]
		vPuti = 3,
		[Description("Частичная отгрузка")]
		chastichnayaOtgruzka = 4,
		[Description("Получено покупателем")]
		poluchenPokupatelem = 5,
		[Description("Завершён")]
		zavershon = 6,
		[Description("Отменён")]
		otmenen = 7,
		[Description("Открытие спора")]
		otkritieSpora = 8,
		[Description("Апелляция")]
		apellyatsiya = 9,
		[Description("Возврат")]
		vozvrat = 10,
		[Description("Отклонение возврата")]
		otkloneniyeVozvrata = 11,
		[Description("Готов к отправке")]
		gotovKOtpravke = 12,
		[Description("Заказ забран")]
		zakazZabran = 13,
		[Description("Архив")]
		archive = 14,
		[Description("Удален")]
		udalen = 15
	}
	public enum DeliveryType
	{
		[Description("Самовывоз")]
		Samovivoz = 0,
		[Description("Доставка платформы")]
		DostavkaPlatformi = 1,
		[Description("Доставка продавцом")]
		DostavkaProdavtsom = 1,
	}
	public enum PaymentType
	{
		[Description("Банковская карта при получении")]
		bankovskayaKartaPriPoluchenii = 0,
		[Description("Наличные при получении")]
		nalichniyePriPoluchenii = 1,
		[Description("Банковская карта онлайн")]
		bankovskayaKartaOnline = 2,
		[Description("Платежная система")]
		platejnayaSistema = 3,
	}
	public enum ProductType
	{
		[Description("Новый")]
		Noviy = 0,
		[Description("Б/У")]
		BU = 1
	}
	public enum ProductStatus
	{
		[Description("Выставлен")]
		Vistavlen = 0,
		[Description("Снят")]
		Snyat = 1,
		[Description("Подтверждён")]
		Podtverzhden = 2,
		[Description("Ожидает проверки")]
		OjidaetProverki = 3,
		[Description("Черновик")]
		Chernovik = 4,
		[Description("Архив")]
		Archive = 5,
		[Description("Удален")]
		udalen = 6,
		[Description("Закончился")]
		zakonchilsya = 7
	}
	public enum SocialType
	{
		[Description("Инстаграмм")]
		instagramm = 0,
		[Description("Одноклассники")]
		odnoklassniki = 1,
		[Description("Фейсбук")]
		facebook = 2,
		[Description("ВК")]
		vk = 3,
		[Description("Ютуб")]
		youtube = 4,
		[Description("Твиттер")]
		twitter = 5,
		[Description("ТикТок")]
		tiktok = 6
	}
}
