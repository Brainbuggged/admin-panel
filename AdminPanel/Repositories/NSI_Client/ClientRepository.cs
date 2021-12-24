using Dapper;
using Npgsql;
using AdminPanel.Models.Models.NSI_Client;
using System;
using System.Data;
using System.Threading.Tasks;
using AdminPanel.Extensions;
using System.Collections.Generic;

namespace AdminPanel.Core.Repositories.NSI_Client
{
	public class ClientRepository
	{
		public string connectionString { get; set; }

		public ClientRepository()
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

		/* GETALL */
		public async Task<IEnumerable<ClientModel>> GetAll()
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QueryAsync<ClientModel>("select * from clients");
			}
		}

		/* GET */
		public async Task<ClientModel> GetByIDAsync(Guid clientId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QuerySingleOrDefaultAsync<ClientModel>("select * from clients where id = " + '\u0027' + clientId + '\u0027');
			}
		}
		public ClientModel GetById(Guid clientId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return dbConnection.QuerySingleOrDefault<ClientModel>("select * from clients where id = " + '\u0027' + clientId + '\u0027');
			}
		}
		public async Task<ClientModel> GetByVendorIdAsync(Guid vendorId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QuerySingleOrDefaultAsync<ClientModel>("select * from clients where vendorid = " + '\u0027' + vendorId + '\u0027');
			}
		}
		public async Task<ClientModel> GetByNumberAsync(string clientNumber)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QuerySingleOrDefaultAsync<ClientModel>("select * from clients where number = " + '\u0027' + clientNumber + '\u0027');
			}
		}
		public async Task<ClientModel> GetByLoginAsync(string login)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QuerySingleOrDefaultAsync<ClientModel>("select * from clients where login = " + '\u0027' + login + '\u0027');
			}
		}
		/* INSERT */
		/* UPDATE */
		/* REMOVE */
		/* COUNT */
	}
}
