﻿using Dapper;
using Npgsql;
using AdminPanel.Models.Models.NSI_Logger;
using System.Data;
using System.Threading.Tasks;
using AdminPanel.Extensions;
using System.Collections.Generic;

namespace AdminPanel.Core.Repositories.NSI_Logger
{
	public class WebLogRepository
	{
		public string connectionString { get; set; }
		public WebLogRepository()
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
		/* GET ALL */
		public async Task<IEnumerable<WebLogModel>> GetAll()
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QueryAsync<WebLogModel>("select * from web_logs");
			}
		}
		/* INSERT */
		public async Task AddAsync(WebLogModel webLog)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync(@"insert into web_logs
					(id, content_length, path, query_string, body, method, date, status_code, remote_ip_address, exemption) 
					VALUES
					(@id, @content_length, @path, @query_string, @body, @method, @date, @status_code, @remote_ip_address, @exemption)"
					, webLog);
			}
		}
		/* UPDATE */
		/* REMOVE */
		/* COUNT */
	}
}
