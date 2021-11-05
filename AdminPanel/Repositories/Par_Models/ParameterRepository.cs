using Dapper;
using Npgsql;
using AdminPanel.Models.Models.Par_Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AdminPanel.Extensions;

namespace AdminPanel.Core.Repositories.Par_Models
{
	public class ParameterRepository
	{
		public string connectionString { get; set; }

		public ParameterRepository()
		{
			connectionString = new SettingsExtension().GetParContextConnectionString();
		}

		internal IDbConnection Connection
		{
			get
			{
				return new NpgsqlConnection(connectionString);
			}
		}

		/* GET */
		public async Task<ParameterModel> GetByNameAndCategoryAsync(Guid categoryId, string parameterName)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QuerySingleOrDefaultAsync<ParameterModel>("select * from parameters where name = " + '\u0027' + parameterName + '\u0027' + " and categoryid = " + '\u0027' + categoryId + '\u0027');
			}
		}
		public async Task<IEnumerable<string>> GetByCategoryAsync(Guid categoryId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QueryAsync<string>("select name from parameters where categoryid = " + '\u0027' + categoryId + '\u0027');
			}
		}
		public async Task<IEnumerable<ParameterModel>> GetAllByCategoryAsync(Guid categoryId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QueryAsync<ParameterModel>("select * from parameters where categoryid = " + '\u0027' + categoryId + '\u0027');
			}
		}
		public async Task<IEnumerable<ParameterModel>> GetIdByCategoryAsync(Guid categoryId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QueryAsync<ParameterModel>("select * from parameters where categoryid = " + '\u0027' + categoryId + '\u0027');
			}
		}
		public IEnumerable<string> GetByCategory(Guid categoryId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return dbConnection.Query<string>("select name from parameters where categoryid = " + '\u0027' + categoryId + '\u0027');
			}
		}
		/* INSERT */
		public async Task AddAsync(ParameterModel newParameter)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync("insert into parameters(id, name, categoryid) VALUES(@id, @name, @categoryid)", newParameter);
			}
		}
		/* UPDATE */
		/* REMOVE */
		/* COUNT */
	}
}
