using Dapper;
using Npgsql;
using AdminPanel.Models.Models.NSI_Client;
using AdminPanel.Models.Models.NSI_Vendor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AdminPanel.Extensions;

namespace AdminPanel.Core.Repositories.NSI_Client
{
	public class FavouriteVendorRepository
	{
		public string connectionString { get; set; }

		public FavouriteVendorRepository()
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
		public async Task<FavouriteVendorModel> GetByVendorAndClientAsync(Guid clientId, Guid vendorId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QuerySingleOrDefaultAsync<FavouriteVendorModel>("select * from favourite_vendors where clientid = " + '\u0027' + clientId + '\u0027' + " and vendorid = " + '\u0027' + vendorId + '\u0027');
			}
		}
		public async Task<bool> CheckInFavouriteAsync(Guid vendorId, Guid clientId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.ExecuteScalarAsync<bool>("select (case when count(*) > 0 then true else false end) from favourite_vendors where vendorid = " + '\u0027' + vendorId + '\u0027' + " and clientid = " + '\u0027' + clientId + '\u0027');
			}
		}
		public bool GetNotifyRequiredByVendorAndClient(Guid vendorId, Guid clientId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return dbConnection.ExecuteScalar<bool>("select is_notify_required from favourite_vendors where vendorid = " + '\u0027' + vendorId + '\u0027' + " and clientid = " + '\u0027' + clientId + '\u0027');
			}
		}
		public async Task<IEnumerable<string>> GetClientIdByVendorIdAsync(Guid vendorId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QueryAsync<string>("select clientid from favourite_vendors where is_notify_required = true and vendorid = " + '\u0027' + vendorId + '\u0027');
			}
		}
		/* INSERT */
		public async Task AddAsync(FavouriteVendorModel favouriteVendor)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync("insert into favourite_vendors(id, is_notify_required, vendorid, clientid) VALUES(@id, @is_notify_required, @vendorid, @clientid)", favouriteVendor);
			}
		}

		/* UPDATE */
		public async Task UpdateNotifyAsync(Guid favouriteVendorId, bool notifyStatus)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync("update favourite_vendors set is_notify_required = " + '\u0027' + notifyStatus + '\u0027' + " where id = " + '\u0027' + favouriteVendorId + '\u0027');
			}
		}
		/* REMOVE */
		public async Task RemoveByIdAsync(Guid favouriteVendorId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync("delete from favourite_vendors where id = " + '\u0027' + favouriteVendorId + '\u0027');
			}
		}

		public async Task<IEnumerable<VendorModel>> GetVendorsAsync(Guid clientId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _request = @"select * from vendors ven "+
					"join "+
					"(select vendorid from favourite_vendors where clientid = " +
					'\u0027' + clientId + '\u0027' +
					") fv on ven.id = fv.vendorid";
				return await dbConnection.QueryAsync<VendorModel>(_request);
			}
		}


		/* COUNT */
	}
}
