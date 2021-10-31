using System;
using System.Collections.Generic;
using Dapper;
using Npgsql;
using AdminPanel.Models.Models.NSI_Order;
using System.Data;
using System.Threading.Tasks;
using AdminPanel.Extensions;
using AdminPanel.ViewModels.Order.GetOrderCard.Response;

namespace AdminPanel.Core.Repositories.NSI_Order
{
	public class OrderStatusChangeRepository
	{
		public string connectionString { get; set; }

		public OrderStatusChangeRepository()
		{
			connectionString = new SettingsExtension().GetAppContextConnectionString();
		}

		internal IDbConnection Connection
		{
			get
			{
				return new NpgsqlConnection(connectionString);
			}
		}

		// /* GET */
		public async Task<IEnumerable<ResponseOrderStatus>> GetByOrderNumberAsync(Guid orderId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QueryAsync<ResponseOrderStatus>(
					"select " +
					"osc.old_status as order_old_status, " +
					"osc.new_status as order_new_status, " +
					"osc.date as change_date " +
					"from order_status_changes osc  " +
					"left join orders ord " +
					"on osc.orderid = ord.id  " +
					"where ord.id = "
					+ '\u0027' + orderId + '\u0027');
			}
		}
		/* INSERT */
		public async Task AddAsync(OrderStatusChangeModel newOrderStatus)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _request =
					@"insert into order_status_changes
					(id, date, old_status, new_status, time, orderid) 
					VALUES
					(@id, @date, @old_status, @new_status, @time, @orderid)";
				await dbConnection.QueryAsync(_request, newOrderStatus);
			}
		}
		/* UPDATE */
		/* REMOVE */
		/* COUNT */
	}
}
