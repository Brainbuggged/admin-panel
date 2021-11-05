using Dapper;
using Npgsql;
using AdminPanel.Extensions;

using AdminPanel.Models.Models.NSI_Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AdminPanel.Core.Repositories.NSI_Client
{
	public class CartItemRepository
	{
		public string connectionString { get; set; }


		public CartItemRepository()
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
		/* GET */
		public async Task<CartItemModel> GetByClientAndProduct(Guid clientId, Guid productId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QuerySingleOrDefaultAsync<CartItemModel>("select * from cart_items where clientid = " + '\u0027' + clientId + '\u0027' + " and productid = " + '\u0027' + productId + '\u0027');
			}
		}
		public async Task<IEnumerable<CartItemModel>> GetAll()
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QueryAsync<CartItemModel>("SELECT * FROM cart_items");
			}
		}
		public async Task<DateTime> GetPaymentDateAsync(Guid clientId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.ExecuteScalarAsync<DateTime>("select adding_date from cart_items where clientid = " + '\u0027' + clientId + '\u0027' + " group by id");
			}
		}
		public async Task<bool> CheckInCartAsync(Guid productId, Guid clientId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.ExecuteScalarAsync<bool>("select (case when count(*) > 0 then true else false end) from cart_items where productid = " + '\u0027' + productId + '\u0027' + " and clientid = " + '\u0027' + clientId + '\u0027');
			}
		}
		
		
		public async Task AddAsync(CartItemModel cartItem)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync("insert into cart_items(id, count, adding_date, productid, clientid) VALUES(@id, @count, @adding_date, @productid, @clientid)", cartItem);
			}
		}
		/* UPDATE */
		public async Task UpdateAsync(CartItemModel cartItem)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync("insert into cart_items(id, count, adding_date, productid, clientid) VALUES(@id, @count, @adding_date, @productid, @clientid)", cartItem);
			}
		}
		public async Task UpdateCountAsync(Guid cartItemId, int count)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync("update cart_items set count = " + '\u0027' + count + '\u0027' + " where id = " + '\u0027' + cartItemId + '\u0027');
			}
		}
		
		/* REMOVE */
		public async Task RemoveAsync(Guid cartItemId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync("delete from cart_items where id = " + '\u0027' + cartItemId + '\u0027');
			}
		}
		public async Task RemoveAllAsync(Guid clientId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync("delete from cart_items where clientid = " + '\u0027' + clientId + '\u0027');
			}
		}

		public async Task ClearCartByClientAsync(Guid clientId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync("delete from cart_items where clientid = " + '\u0027' + clientId + '\u0027');
			}
		}
		/* COUNT */
		public async Task<int> CountClientCarts(Guid clientId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.ExecuteScalarAsync<int>("select sum (count) FROM cart_items where clientid = " + '\u0027' + clientId + '\u0027');
			}
		}
	}
}
