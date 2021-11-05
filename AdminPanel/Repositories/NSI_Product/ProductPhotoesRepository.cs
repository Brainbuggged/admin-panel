using Dapper;
using Npgsql;
using AdminPanel.Models.Models.NSI_Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AdminPanel.Extensions;
using AdminPanel.Models.Catalog.GetProductCard.Response;
using AdminPanel.ViewModels.Product.CreateProduct.Query;

namespace AdminPanel.Core.Repositories.NSI_Product
{
	public class ProductPhotoesRepository
	{
		public string connectionString { get; set; }

		public ProductPhotoesRepository()
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
		public async Task<IEnumerable<ResponseProductPhoto>> GetByProductAsync(Guid productId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _request = "select linq as photo from product_photoes where productid = " + '\u0027' + productId + '\u0027';
				return await dbConnection.QueryAsync<ResponseProductPhoto>(_request);
			}
		}
		public async Task<IEnumerable<createProductPhoto>> GetDraftProductAsync(Guid productId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _request = "select linq, is_main as photo from product_photoes where productid = " + '\u0027' + productId + '\u0027';
				return await dbConnection.QueryAsync<createProductPhoto>(_request);
			}
		}
		public Task GetByID(Guid id)
		{
			throw new NotImplementedException();
		}
		public async Task<string> GetProductMainPhotoAsync(Guid productId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.ExecuteScalarAsync<string>("select linq from product_photoes where productid = " + '\u0027' + productId + '\u0027' + " and is_main = " + '\u0027' + true + '\u0027');
			}
		}
		/* INSERT */
		public void Add(ProductPhotoModel photo)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				dbConnection.Query("insert into product_photoes(id, is_main, linq, productid) VALUES(@id, @is_main, @linq, @productid)", photo);
			}
		}
		/* UPDATE */
		/* REMOVE */
		public async Task RemoveByProductIdAsync(Guid productId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync("delete from product_photoes where productid = " + '\u0027' + productId + '\u0027');
			}
		}
		/* COUNT */
	}
}
