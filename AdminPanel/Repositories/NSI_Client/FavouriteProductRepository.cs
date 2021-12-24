using Dapper;
using Npgsql;
using AdminPanel.Models.Models.NSI_Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AdminPanel.Extensions;

namespace AdminPanel.Core.Repositories.NSI_Client
{
	public class FavouriteProductRepository
	{
		public string connectionString { get; set; }

		public FavouriteProductRepository()
		{
			connectionString = new SettingsLibrary.ConnectionSettings().GetAppContextConnectionString();
		}

		internal IDbConnection Connection
		{
			get
			{
				return new NpgsqlConnection(connectionString);
			}
		}

		/* GET */
		public async Task<FavouriteProductModel> GetByProductAndClientAsync(Guid clientId, Guid productId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QuerySingleOrDefaultAsync<FavouriteProductModel>("select * from favourite_products where clientid = " + '\u0027' + clientId + '\u0027' + " and productid = " + '\u0027' + productId + '\u0027');
			}
		}

		public async Task<IEnumerable<Guid>> GetClientIdByProductIdAsync(Guid productId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QueryAsync<Guid>("select clientid from favourite_products where is_notify_required = true and productid = " + '\u0027' + productId + '\u0027');
			}
		}

		public async Task<bool> CheckInFavouriteAsync(Guid productId, Guid clientId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.ExecuteScalarAsync<bool>("select (case when count(*) > 0 then true else false end) from favourite_products where productid = " + '\u0027' + productId + '\u0027' + " and clientid = " + '\u0027' + clientId + '\u0027');
			}
		}
		/* INSERT */
		public async Task AddAsync(FavouriteProductModel favouriteProduct)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync("insert into favourite_products(id, is_notify_required, productid, clientid) VALUES(@id, @is_notify_required, @productid, @clientid)", favouriteProduct);
			}
		}

		/* UPDATE */
		public async Task UpdateNotifyAsync(Guid favouriteProductId, bool notifyStatus)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync("update favourite_products set is_notify_required = " + '\u0027' + notifyStatus + '\u0027' + " where id = " + '\u0027' + favouriteProductId + '\u0027');
			}
		}
		/* REMOVE */
		public async Task RemoveByIdAsync(Guid favouriteProductId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync("delete from favourite_products where id = " + '\u0027' + favouriteProductId + '\u0027');
			}
		}

		// public async Task<IEnumerable<ResponeFavouriteProduct>> GetProductsAsync(Guid clientId)
		// {
		// 	using (IDbConnection dbConnection = Connection)
		// 	{
		// 		dbConnection.Open();
		// 		var _request = "select " +
		// 		"pr.publication_date as product_publish_date, " +
		// 		"pr.name as product_name, " +
		// 		"pr.number as product_number, " +
		// 		"pr.city as product_city, " +
		// 		"pr.country as product_country, " +
		// 		"pr.type as product_type, " +
		// 		"pr.brand as product_brand, " +
		// 		"pr.rating as product_rating, " +
		// 		"pr.id as product_id, " +
		// 		"pr.prise as product_prise, " +
		// 		"pr.ru_name as product_category, " +
		// 		"(case when pr.in_cart = true then true else false end) as is_in_cart,  " +
		// 		"pp.linq as product_photo, " +
		// 		"(case when fp.is_notify_required = true then true else false end) as is_notify_required,  " +
		// 		"pr.count as product_amount " +
		// 		"from " +
		// 		"(select " +
		// 		"pr.id, " +
		// 		"pr.publication_date, " +
		// 		"pr.name, " +
		// 		"pr.country, " +
		// 		"pr.city, " +
		// 		"pr.brand, " +
		// 		"pr.prise, " +
		// 		"pr.number, " +
		// 		"pr.type, " +
		// 		"pr.rating, " +
		// 		"sq.in_cart, " +
		// 		"pc.ru_name, " +
		// 		"pr.count " +
		// 		"from products pr " +
		// 		"left join " +
		// 		"(select pr.id, true as in_cart from products pr where pr.id in " +
		// 		"(select productid from cart_items where clientid = "
		// 		+ '\u0027' + clientId + '\u0027' +
		// 		")) sq " +
		// 			"on pr.id = sq.id " +
		// 			"join product_categories pc " +
		// 			"on pr.categoryid = pc.id) pr " +
		// 			"left join " +
		// 			"(select productid, linq from product_photoes pp where pp.is_main = true) pp " +
		// 			"on pp.productid = pr.id " +
		// 			"join " +
		// 			"(select productid, is_notify_required from favourite_products where clientid = "
		// 			+ '\u0027' + clientId + '\u0027' +
		// 			" ) fp on pr.id = fp.productid";
		// 		return await dbConnection.QueryAsync<ResponeFavouriteProduct>(_request);
		// 	}
		// }

		/* COUNT */
	}
}
