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
	public class ProductPropertiesRepository
	{
		public string connectionString { get; set; }

		public ProductPropertiesRepository()
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
		public async Task<IEnumerable<ResponseProductProperty>> GetByProductAsync(Guid productId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QueryAsync<ResponseProductProperty>("select name, value from product_properties where productid = " + '\u0027' + productId + '\u0027');
			}
		}
		public async Task<IEnumerable<createProductProperty>> GetDraftPropertiesAsync(Guid productId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QueryAsync<createProductProperty>("select name as property_name, value as property_value from product_properties where productid = " + '\u0027' + productId + '\u0027');
			}
		}
		public IEnumerable<string> GetByParameterAndCategory(string parameterName, string categoryName)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return dbConnection.Query<string>(
					@"select distinct value from product_properties pp join 
						( select pr.id from products pr join product_categories pc on pr.categoryid = pc.id where pc.en_name = "
						+ '\u0027' + categoryName + '\u0027' +
						" )pr on pp.productid = pr.id" +
						" where pp.name = " + '\u0027' + parameterName + '\u0027');
			}
		}

		/* INSERT */
		public void Add(ProductPropertyModel prop)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				dbConnection.Query("insert into product_properties(id, name, value, alternative_value, productid) VALUES(@id, @name, @value, @alternative_value, @productid)", prop);
			}
		}
		/* UPDATE */
		/* REMOVE */
		public async Task RemoveByProductIdAsync(Guid productId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync("delete from product_properties where productid = " + '\u0027' + productId + '\u0027');
			}
		}
		/* COUNT */

	}
}
