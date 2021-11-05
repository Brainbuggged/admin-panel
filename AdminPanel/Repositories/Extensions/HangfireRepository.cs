using Dapper;
using Npgsql;
using AdminPanel.Extensions;
using AdminPanel.Models;
using AdminPanel.Models.Models.NSI_Client;
using AdminPanel.Models.Models.NSI_Vendor;
using System;
using System.Data;
using System.Threading.Tasks;

namespace AdminPanel.Repositories.Extensions
{
	public class HangfireRepository
	{
		public string connectionString { get; set; }

		public HangfireRepository()
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
		// public async Task<IEnumerable<ZeroProductToEmail>> GetZeroProducts()
		// {
		// 	using (IDbConnection dbConnection = Connection)
		// 	{
		// 		dbConnection.Open();
		// 		var _requst =
		// 			@"select pr.name as product_name, pr.number as product_number, concat(ven.surname, ' ', ven.name, ' ', ven.patronymic) as vendor_fio, ven.email as vendor_email
		// 			from products pr
		// 			left join vendors ven on pr.vendorid = ven.id
		// 			where pr.status = " + (int)ProductStatus.zakonchilsya;
		// 		return await dbConnection.QueryAsync<ZeroProductToEmail>(_requst);
		// 	}
		// }
		// public async Task<IEnumerable<dbOrderToClose>> GetOrdersToCloseAsync()
		// {
		// 	using (IDbConnection dbConnection = Connection)
		// 	{
		// 		dbConnection.Open();
		// 		var _requst =
		// 			@"select id, osc.date as date from orders ord
		// 			left join " +
		// 			$"(select date, orderid from order_status_changes where new_status in ({(int)OrderStatus.zakazZabran}, {(int)OrderStatus.poluchenPokupatelem})) osc " +
		// 			@"on ord.id = osc.orderid "+
		// 			$"where ord.status in ({(int)OrderStatus.zakazZabran}, {(int)OrderStatus.poluchenPokupatelem})";
		// 		return await dbConnection.QueryAsync<dbOrderToClose>(_requst);
		// 	}
		// }
		// public async Task<IEnumerable<ChatMessageModel>> GetUnreadMessagesAsync()
		// {
		// 	using (IDbConnection dbConnection = Connection)
		// 	{
		// 		dbConnection.Open();
		// 		var _request = "select * from chat_messages where @Date - date >= interval '0 day 0:01:00' and is_read = false";
		// 		DynamicParameters dp = new DynamicParameters();
		// 		dp.Add("@Date", new SettingsExtensions().GetDateTimeNow(), DbType.DateTime);
		//
		// 		return await dbConnection.QueryAsync<ChatMessageModel>(_request, dp);
		// 	}
		// }
		public async Task<ClientModel> GetClientAsync(Guid clientId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _requst = "select surname, name, patronymic, email from clients where id = " + '\u0027' + clientId + '\u0027';
				return await dbConnection.QuerySingleAsync<ClientModel>(_requst);
			}
		}
		public async Task<VendorModel> GetVendorAsync(Guid vendorId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _requst = "select surname, name, patronymic, email from vendors where id = " + '\u0027' + vendorId + '\u0027';
				return await dbConnection.QuerySingleAsync<VendorModel>(_requst);
			}
		}
		// public async Task RemoveAllExpiredCartsAsync()
		// {
		// 	using (IDbConnection dbConnection = Connection)
		// 	{
		// 		dbConnection.Open();
		// 		var _request = "delete from cart_items where @Date - adding_date >= interval '0 day 0:30:00'";
		// 		DynamicParameters dp = new DynamicParameters();
		// 		dp.Add("@Date", new SettingsExtensions().GetDateTimeNow(), DbType.DateTime);
		// 		await dbConnection.QueryAsync(_request, dp);
		// 	}
		// }
		public async Task RemoveAllZeroCartsAsync()
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _request = $"delete from cart_items ci where ci.productid in (select id from products where status = {(int)ProductStatus.zakonchilsya})";
				await dbConnection.QueryAsync(_request);
			}
		}
		public async Task AutoChangeCartCountAsync()
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _request =
				$"update сart_items ci " +
					"set count = pr.amount" +
					" from(select ci.id" +
					" as id, pr.amount" +
					" from cart_items ci" +
					" join(select count" +
					" as amount, id" +
					" from products) pr" +
					" on ci.productid = pr.id" +
					" where ci.count > pr.amount) pr" +
					" where ci.id = pr.id";
				await dbConnection.QueryAsync(_request);
			}
		}
		public async Task AutoCloseOrder(Guid orderId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync($"update orders set status = {(int)OrderStatus.zavershon} where id = " + '\u0027' + orderId + '\u0027');
			}
		}
		// public async Task ArchiveZeroProduct()
		// {
		// 	using (IDbConnection dbConnection = Connection)
		// 	{
		// 		dbConnection.Open();
		// 		DynamicParameters dp = new DynamicParameters();
		// 		dp.Add("@Date", new SettingsExtension().GetDateTimeNow(), DbType.DateTime);
		// 		var _request = $"update products set status = {(int)ProductStatus.Archive} where @Date - last_status_update >= interval '14 day 0:00:00' and status = {(int)ProductStatus.zakonchilsya}";
		// 		await dbConnection.QueryAsync(_request);
		// 	}
		// }
		// public async Task RemoveArchiveProduct()
		// {
		// 	using (IDbConnection dbConnection = Connection)
		// 	{
		// 		dbConnection.Open();
		// 		DynamicParameters dp = new DynamicParameters();
		// 		dp.Add("@Date", new SettingsExtension().GetDateTimeNow(), DbType.DateTime);
		// 		var _request = $"update products set status = {(int)ProductStatus.udalen} where @Date - last_status_update >= interval '60 day 0:00:00' and status = {(int)ProductStatus.Archive}";
		// 		await dbConnection.QueryAsync(_request);
		// 	}
		// }
		// public async Task RemoveDraftProduct()
		// {
		// 	using (IDbConnection dbConnection = Connection)
		// 	{
		// 		dbConnection.Open();
		// 		DynamicParameters dp = new DynamicParameters();
		// 		dp.Add("@Date", new SettingsExtension().GetDateTimeNow(), DbType.DateTime);
		// 		var _request = $"update products set status = {(int)ProductStatus.udalen} where @Date - last_status_update >= interval '30 day 0:00:00' and status = {(int)ProductStatus.Chernovik}";
		// 		await dbConnection.QueryAsync(_request);
		// 	}
		// }


		// public async Task RemoveArchiveOrder()
		// {
		// 	using (IDbConnection dbConnection = Connection)
		// 	{
		// 		dbConnection.Open();
		// 		DynamicParameters dp = new DynamicParameters();
		// 		dp.Add("@Date", new SettingsExtension().GetDateTimeNow(), DbType.DateTime);
		// 		var _request = $"update orders set status = {(int)OrderStatus.udalen} where @Date - last_status_update >= interval '60 day 0:00:00' and status = {(int)OrderStatus.archive}";
		// 		await dbConnection.QueryAsync(_request);
		// 	}
		// }
		public async Task UpdateVendorRatingAsync()
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _requst =
					@"UPDATE vendors ven
					SET 
					rating = 
					(select 
					round( (cast(sum(vendor_rating) as numeric)/count(*)), 2)
					from product_comments pc
					left join 
					products pr
					on pc.productid = pr.id
					where vendorid = ven.id),
					comment_count = 
					(select 
					count(*)
					from product_comments pc
					left join 
					products pr
					on pc.productid = pr.id
					where vendorid = ven.id)";
				await dbConnection.QueryAsync(_requst);
			}
		}
		public async Task UpdateProductRatingAsync()
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _requst =
					@"UPDATE products pr
					set rating =
					(select 
					round( (cast(sum(rating) as numeric)/count(*)), 2)
					from product_comments pc
					where pc.productid = pr.id)";
				await dbConnection.QueryAsync(_requst);
			}
		}
		public void UpdateVendorRating()
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _requst =
					@"UPDATE vendors ven
					SET 
					rating = 
					(select 
					round( (cast(sum(vendor_rating) as numeric)/count(*)), 2)
					from product_comments pc
					left join 
					products pr
					on pc.productid = pr.id
					where vendorid = ven.id),
					comment_count = 
					(select 
					count(*)
					from product_comments pc
					left join 
					products pr
					on pc.productid = pr.id
					where vendorid = ven.id)";
				dbConnection.Query(_requst);
			}
		}
		public void UpdateProductRating()
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _requst =
					@"UPDATE products pr
					set rating =
					(select 
					round( (cast(sum(rating) as numeric)/count(*)), 2)
					from product_comments pc
					where pc.productid = pr.id)";
				dbConnection.Query(_requst);
			}
		}
	}
}
