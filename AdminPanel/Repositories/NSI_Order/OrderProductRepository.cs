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
	public class OrderProductRepository
	{
		public string connectionString { get; set; }

		public OrderProductRepository()
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
		public async Task<IEnumerable<ResponseOrderProduct>> GetByOrderNumberAndVendorIdAsync(int orderNumber, Guid vendorId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QueryAsync<ResponseOrderProduct>(
					"select " +
					"pr.publication_date as product_publication_date, " +
					"pr.number as product_number, " +
					"pr.photo as product_photo, " +
					"pr.name as product_name, " +
					"pr.country as product_country, " +
					"pr.city as product_city, " +
					"pr.category_name as product_category, " +
					"pr.brand as product_brand, " +
					"pr.prise as product_prise, " +
					"pr.count as product_count, " +
					"pr.total_prise as product_total_cost " +
					"from order_products pr  " +
					"left join orders ord " +
					"on pr.orderid = ord.id  " +
					"where ord.number = "
					+ '\u0027' + orderNumber + '\u0027' +
					@" and vendorid = "
					+ '\u0027' + vendorId + '\u0027');
			}
		}
		public async Task<IEnumerable<ResponseOrderProduct>> GetByOrderNumberAsync(Guid orderId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QueryAsync<ResponseOrderProduct>(
					"select " +
					"pr.publication_date as product_publication_date, " +
					"pr.product_number as product_number, " +
					"pr.photo as product_photo, " +
					"pr.name as product_name, " +
					"pr.country as product_country, " +
					"pr.city as product_city, " +
					"pr.category_name as product_category, " +
					"pr.brand as product_brand, " +
					"pr.prise as product_prise, " +
					"pr.count as product_count, " +
					"pr.total_prise as product_total_cost " +
					"from order_products pr  " +
					"left join orders ord " +
					"on pr.orderid = ord.id  " +
					"where ord.id = "
					+ '\u0027' + orderId + '\u0027');
			}
		}
		/* INSERT */
		public async Task AddAsync(OrderProductModel newOrderProduct)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _request =
					@"insert into order_products
					(id, product_number, product_id, publication_date, photo, name, country, city, category_name, brand, prise, count, total_prise, orderid, categoryid) 
					VALUES
					(@id, @product_number, @product_id, @publication_date, @photo, @name, @country, @city, @category_name, @brand, @prise, @count, @total_prise, @orderid, @categoryid)";
				await dbConnection.QueryAsync(_request, newOrderProduct);
			}
		}
		/* UPDATE */
		/* REMOVE */
		/* COUNT */
	}
}
