using Dapper;
using Npgsql;
using AdminPanel.Models.Models.NSI_Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AdminPanel.Extensions;
using AdminPanel.Models.Catalog.GetCategories.Response;

namespace AdminPanel.Core.Repositories.NSI_Product
{
	public class ProductCategoryRepository
	{
		public string connectionString { get; set; }

		public ProductCategoryRepository()
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
		/* GET ALL */
		public async Task<IEnumerable<ProductCategoryModel>> GetAll()
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QueryAsync<ProductCategoryModel>("select * from product_categories");
			}
		}
		/* GET */
		public async Task<string> GetByIDAsync(Guid categoryId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.ExecuteScalarAsync<string>("select * from product_categories where ru_name = " + '\u0027');
			}
		}
		public Task<ProductCategoryModel> GetByNumber(int number)
		{
			throw new NotImplementedException();
		}
		public async Task<ProductCategoryModel> GetByNameAsync(string ruName)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QuerySingleOrDefaultAsync<ProductCategoryModel>("select * from product_categories where ru_name = " + '\u0027' + ruName + '\u0027' + " or en_name = " + '\u0027' + ruName + '\u0027');
			}
		}
		public async Task<IEnumerable<ResponseCategory>> GetAllAsync()
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QueryAsync<ResponseCategory>("select is_last, ru_name, en_name, photo from product_categories where parentid = " + '\u0027' + "" + '\u0027');
			}
		}
		public async Task<IEnumerable<ResponseCategory>> GetChildrenAsync(Guid categoryId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QueryAsync<ResponseCategory>(
					@"select is_last, ru_name, en_name, photo from product_categories where parentid = " + '\u0027' + categoryId + '\u0027');
			}
		}
		/* ADD */
		public async Task AddAsync(ProductCategoryModel newCategory)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync("insert into product_categories(id, ru_name, en_name, parentid, is_last, photo) VALUES(@id, @ru_name, @en_name, @parentid, @is_last, @photo)", newCategory);
			}
		}
		/* UPDATE */
		public async Task UpdateParentAsync(Guid parentId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync("update product_categories set is_last = " + '\u0027' + false + '\u0027' + " where id = " + '\u0027' + parentId + '\u0027');
			}
		}
		/* REMOVE */
		/* COUNT */
	}
}
