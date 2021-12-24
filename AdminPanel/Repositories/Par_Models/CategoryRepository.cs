using Dapper;
using Npgsql;
using AdminPanel.Models.Models.Par_Models;
using System;
using System.Data;
using System.Threading.Tasks;
using AdminPanel.Extensions;

namespace AdminPanel.Core.Repositories.Par_Models
{
	public class CategoryRepository
	{
		public string connectionString { get; set; }

		public CategoryRepository()
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
		public async Task<CategoryModel> GetByNameAsync(string name)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QuerySingleOrDefaultAsync<CategoryModel>("select * from categories where name = " + '\u0027' + name.ToUpper() + '\u0027' + " or en_name = " + '\u0027' + name.ToLower() + '\u0027');
			}
		}
		public async Task<CategoryModel> GetByIdAsync(Guid categoryId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QuerySingleOrDefaultAsync<CategoryModel>("select * from categories where id = " + '\u0027' + categoryId + '\u0027');
			}
		}
		public Guid GetId(string name)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return dbConnection.ExecuteScalar<Guid>("select id from categories where name = " + '\u0027' + name + '\u0027' + " or en_name = " + '\u0027' + name + '\u0027');
			}
		}
		/* INSERT */
		public async Task AddAsync(CategoryModel newCategory)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync("insert into categories(id, name, en_name) VALUES(@id, @name, @en_name)", newCategory);
			}
		}
		/* UPDATE */
		/* REMOVE */
		/* COUNT */

	}
}
