using Dapper;
using Npgsql;
using AdminPanel.Models;
using AdminPanel.Models.Models.NSI_Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AdminPanel.Extensions;
using AdminPanel.ViewModels.Order.GetOrderCard.Response;
using AdminPanel.ViewModels.Order.GetVendorOrders.Response;

namespace AdminPanel.Core.Repositories.NSI_Order
{
	public class OrderRepository
	{
		public string connectionString { get; set; }

		public OrderRepository()
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
		public async Task<IEnumerable<OrderForGrouping>> GetByClientIdAsync(Guid clientId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _request =
					@"select " +
					"ord.date, " +
					"ord.id, " +
					"ord.number, " +
					"ord.position_count, " +
					"ord.total_prise, " +
					"ord.status, " +
					"concat(ven.surname, "
					+ '\u0027' + '\u0027' +
					",ven.name,"
					+ '\u0027'+'\u0027' +
					@",ven.patronymic) as vendor_name " +
					"from orders ord " +
					"left join " +
					"vendors ven " +
					"on ord.vendorid = " +
					"ven.id " +
					"where ord.clientid = " + '\u0027' + clientId + '\u0027';
				return await dbConnection.QueryAsync<OrderForGrouping>(_request);
			}
		}
		public async Task<IEnumerable<ResponseVendorOrder>> GetByVendorIdAsync(Guid vendorid)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QueryAsync<ResponseVendorOrder>(
					@"select 
					ord.number as order_number, 
					cl.name as client_name,
					ord.date as order_date, 
					ord.id as order_id
					ord.position_count as order_position_count, 
					ord.status as order_status, 
					ord.total_prise as order_total_prise 
					from orders ord
					left join
					clients cl
					on ord.clientid = cl.id
					where ord.vendorid = "
					+ '\u0027' + vendorid + '\u0027');
			}
		}
		public async Task<ResponseOrderCard> GetOrderCard(Guid clientId, Guid vendorid, Guid orderId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _request =
					@"select 
					id as order_id,
					number as order_number,
					date as order_date, 
					delivery_type as order_delivery_type, 
					status as order_status, 
					address as order_address,
					status as order_status,
					(case when vendorid = "
					+ '\u0027' + vendorid + '\u0027' +
					" then "
					+ '\u0027' + "Продавец" + '\u0027' +
					"else "
					+'\u0027' + "Покупатель" + '\u0027' +
					@"end) as order_role
					from orders
					where (vendorid = "
					+ '\u0027' + vendorid + '\u0027' +
					@"or clientid = "
					+ '\u0027' + clientId + '\u0027' +
					@") and id = "
					+ '\u0027' + orderId + '\u0027';
				return await dbConnection.QueryFirstOrDefaultAsync<ResponseOrderCard>(_request);
			}
		}

		public async Task<OrderModel> GetByIdAsync(Guid orderId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QuerySingleOrDefaultAsync<OrderModel>("select number, clientid, vendorid from orders where id = " + '\u0027' + orderId + '\u0027');
			}
		}

		// public async Task<ResponseOrderCard> GetOrderCard(Guid clientId, Guid orderId)
		// {
		// 	using (IDbConnection dbConnection = Connection)
		// 	{
		// 		dbConnection.Open();
		// 		var _request =
		// 			@"select 
		// 			number as order_number,
		// 			date as order_date, 
		// 			delivery_type as order_delivery_type, 
		// 			status as order_status,
		// 			address as order_address, "
		// 			+ '\u0027' + "Покупатель" + '\u0027' +
		// 			@" as order_role 
		// 			from orders
		// 			where clientid = "
		// 			+ '\u0027' + clientId + '\u0027' +
		// 			@" and id = "
		// 			+ '\u0027' + orderId + '\u0027';
		// 		return await dbConnection.QueryFirstOrDefaultAsync<ResponseOrderCard>(_request);
		// 	}
		// }
		public async Task<OrderModel> GetByNumberAndClientAsync(Guid orderId, Guid clientId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QuerySingleOrDefaultAsync<OrderModel>($"select * from orders where id = " + '\u0027' + orderId + '\u0027' + " and clientid = " + '\u0027' + clientId + '\u0027');
			}
		}
		public async Task<OrderModel> GetByNumberAndVendorAsync(Guid orderId, Guid vendorId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QuerySingleOrDefaultAsync<OrderModel>($"select * from orders where id = " + '\u0027' + orderId + '\u0027' + " and vendorid = " + '\u0027' + vendorId + '\u0027');
			}
		}
		/* ADD */
		public async Task AddAsync(OrderModel newOrder)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _request =
					@"insert into orders
					(id, number, positions_prise, delivery_prise, total_prise, position_count, x, y, address, date, status, delivery_type, payment_type, vendorid, clientid) 
					VALUES
					(@id, @number, @positions_prise, @delivery_prise, @total_prise, @position_count, @x, @y, @address, @date, @status, @delivery_type, @payment_type, @vendorid, @clientid)";
				await dbConnection.QueryAsync(_request, newOrder);
			}
		}
		/* UPDATE */
		public async Task UpdateStatus(Guid orderId, OrderStatus newStatus)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _request = "update orders set status = @Status where id = " + '\u0027' + orderId + '\u0027';
				DynamicParameters dp = new DynamicParameters();
				dp.Add("@Status", (int)newStatus, DbType.Int32);
				await dbConnection.QueryAsync(_request, dp);
			}
		}
		/* REMOVE */
		/* COUNT */
		public async Task<int> CountAsync()
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.ExecuteScalarAsync<int>("select count(*) from orders");
			}
		}

	}
}
