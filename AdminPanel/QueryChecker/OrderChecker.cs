using AdminPanel.ViewModels.Order.ChangeOrderStatus.Query;
using AdminPanel.ViewModels.Order.CreateOrder.Query;
using AdminPanel.ViewModels.Order.GetOrderCard.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminPanel.Extensions;

namespace AdminPanel.QueryChecker
{
	public class OrderChecker
	{

		public CustomBadRequest Check_CreateOrder(createOrder query)
		{
			List<string> errors = new List<string>();

			if (int.TryParse(query.deliveryType.ToString(), out int a) == false)
				errors.Add($"Тип данных для deliveryType должен быть int");
			else
				if (int.Parse(query.deliveryType.ToString()) < 0 || int.Parse(query.deliveryType.ToString()) > 2)
				errors.Add($"Значение deliveryType должно быть от 0 до 2");

			if (int.TryParse(query.paymentType.ToString(), out a) == false)
				errors.Add($"Тип данных для PaymentType должен быть int");
			else
				if (int.Parse(query.paymentType.ToString()) < 0 || int.Parse(query.deliveryType.ToString()) > 3)
				errors.Add($"Значение PaymentType должно быть от 0 до 3");

			if (String.IsNullOrWhiteSpace(query.deliveryAddress))
				errors.Add($"Параметр deliveryAddress явяляется обязательным для заполнения");

			if (double.TryParse(query.x.ToString(), out double b) == false)
				errors.Add($"Тип данных для x должен быть double");

			if (double.TryParse(query.y.ToString(), out b) == false)
				errors.Add($"Тип данных для y должен быть double");

			if (errors.Count != 0)
				return new CustomBadRequest("При валидации данных произошла ошибка", errors);
			else
				return null;
		}

		public CustomBadRequest Check_GetOrderCard(getOrderCard query)
		{
			List<string> errors = new List<string>();

			if (Guid.TryParse(query.orderId.ToString(), out Guid a) == false)
				errors.Add($"Тип данных для orderId должен быть uuid");

			if (errors.Count != 0)
				return new CustomBadRequest("При валидации данных произошла ошибка", errors);
			else
				return null;
		}

		public CustomBadRequest Check_ChangeOrderStatus(changeOrderStatus query)
		{
			List<string> errors = new List<string>();

			if (Guid.TryParse(query.orderId.ToString(), out Guid a) == false)
				errors.Add($"Тип данных для orderId должен быть uuid");

			if (String.IsNullOrWhiteSpace(query.role))
				errors.Add($"Параметр role явяляется обязательным для заполнения");
			else
				if (query.role != "Продавец" && query.role != "Покупатель")
					errors.Add($"Значение role должно быть 'Продавец' или 'Клиент'");


			if (String.IsNullOrWhiteSpace(query.status))
				errors.Add($"Параметр status явяляется обязательным для заполнения");
			else
				if (query.status != "В обработке" && 
					query.status != "Ожидается отмена" && 
					query.status != "Ожидается отправка" && 
					query.status != "В пути" && 
					query.status != "Частичная отгрузка" && 
					query.status != "Получено покупателем" && 
					query.status != "Завершён" && 
					query.status != "Отменён" && 
					query.status != "Открытие спора" && 
					query.status != "Апелляция" && 
					query.status != "Возврат" &&
					query.status != "Отклонение возврата" &&
					query.status != "Готов к отправке" &&
					query.status != "Заказ забран" &&
					query.status != "Архив" &&
					query.status != "")
					errors.Add($"Не корретное значение для status");

			if (errors.Count != 0)
				return new CustomBadRequest("При валидации данных произошла ошибка", errors);
			else
				return null;
		}
	}
}
