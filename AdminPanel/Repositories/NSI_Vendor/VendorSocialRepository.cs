using System;
using System.Collections.Generic;
using Npgsql;
using System.Data;
using System.Threading.Tasks;
using AdminPanel.Extensions;
using AdminPanel.Models.Catalog.GetVendorCard.Response;
using Dapper;

namespace AdminPanel.Core.Repositories.NSI_Vendor
{
	public class VendorSocialRepository
	{
		public string connectionString { get; set; }

		public VendorSocialRepository()
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
		public async Task<IEnumerable<ResponseSocialMedia>> GetByVendorAsync(Guid vendorId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QueryAsync<ResponseSocialMedia>("select type as name, linq from vendor_socials where vendorid = " + '\u0027' + vendorId + '\u0027');
			}
		}
		/* INSERT */
		/* UPDATE */
		/* REMOVE */
		/* COUNT */
	}
}
