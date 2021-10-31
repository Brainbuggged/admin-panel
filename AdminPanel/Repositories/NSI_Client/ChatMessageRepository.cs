using Dapper;
using Npgsql;
using AdminPanel.Extensions;
using AdminPanel.Models;
using AdminPanel.Models.Models.NSI_Client;
using System;
using System.Data;
using System.Threading.Tasks;

namespace AdminPanel.Core.Repositories.NSI_Client
{
	public class ChatMessageRepository
	{
		public string connectionString { get; set; }

		public ChatMessageRepository()
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

		/* INSERT */
		public async Task AddAsync(ChatMessageModel newMessage)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync("insert into chat_messages(id, date, is_read, text, clientid, vendorid, reciever) VALUES(@id, @date, @is_read, @text, @clientid, @vendorid, @reciever)", newMessage);
			}
		}
		/* UPDATE */
		public async Task UpdateIsReadAsync(Guid clientId, Guid vendorId, RoleType roleType)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _request = "update chat_messages set is_read = @Read where clientid = " + '\u0027' + clientId + '\u0027' + " and vendorid = " + '\u0027' + vendorId + '\u0027' + $" and reciever = {(int)roleType}";
				DynamicParameters dp = new DynamicParameters();
				dp.Add("@Read", true, DbType.Boolean);
				await dbConnection.QueryAsync(_request, dp);
			}
		}
		/* REMOVE */
		/* COUNT */
	}
}
