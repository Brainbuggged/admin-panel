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
	public class ParameterValueRepository
	{
		public string connectionString { get; set; }

		public ParameterValueRepository()
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
		public IEnumerable<string> GetByParameterAsync(Guid parameterId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return dbConnection.Query<string>("select value from parameter_values where parameterid = " + '\u0027' + parameterId + '\u0027');
			}
		}
		public ParameterValueModel GetByValueAndParameter(Guid parameterId, string value)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return dbConnection.QuerySingleOrDefault<ParameterValueModel>("select * from parameter_values where value = " + '\u0027' + value + '\u0027' + "and parameterid = " + '\u0027' + parameterId + '\u0027');
			}
		}
		/* INSERT */
		public async Task AddAsync(ParameterValueModel newParValue)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync("insert into parameter_values(id, value, parameterid) VALUES(@id, @value, @parameterid)", newParValue);
			}
		}
		public void Add(ParameterValueModel newParValue)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				dbConnection.Query("insert into parameter_values(id, value, parameterid) VALUES(@id, @value, @parameterid)", newParValue);
			}
		}
		/* UPDATE */
		/* REMOVE */
		/* COUNT */
	}
}
