using Dapper;
using Npgsql;
using AdminPanel.Models.Models.NSI_Vendor;
using System;
using System.Data;
using System.Threading.Tasks;
using AdminPanel.Extensions;
using AdminPanel.Models.Catalog.GetProductCard.Response;

namespace AdminPanel.Core.Repositories.NSI_Vendor
{
	public class VendorRepository
	{
		public string connectionString { get; set; }

		public VendorRepository()
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
		public async Task<ResponseProductVendor> GetByIdAsync(Guid vendorId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QuerySingleOrDefaultAsync<ResponseProductVendor>(@"select 
					id as vendor_id,
					number as vendor_number, 
					concat(name ,' ', surname ,' ', patronymic) as vendor_fio, 
					phone as vendor_phone, 
					is_fiz_face, 
					is_ur_face, 
					rating as vendor_rating 
					from vendors where id = "
					+ '\u0027' + vendorId + '\u0027');
			}
		}
		public async Task<VendorModel> GetVendorByIdAsync(Guid vendorId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QuerySingleOrDefaultAsync<VendorModel>("select * from vendors where id = "+ '\u0027' + vendorId + '\u0027');
			}
		}
		public async Task<VendorModel> GetByNumberAsync(string vendorNumber)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QuerySingleOrDefaultAsync<VendorModel>("select * from vendors where number = " + '\u0027' + vendorNumber + '\u0027');
			}
		}
		public async Task<double> GetRatingAsync(Guid vendorId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.ExecuteScalarAsync<double>("select rating from vendors where id = " + '\u0027' + vendorId + '\u0027');
			}
		}
		/* INSERT */
		/* UPDATE */
		// public async Task UpdateAssortimentStatusAsync(Guid vendorId, bool isAssortimentUpdated)
		// {
		// 	using (IDbConnection dbConnection = Connection)
		// 	{
		// 		dbConnection.Open();
		// 		DynamicParameters dp = new DynamicParameters();
		// 		dp.Add("@Date", new SettingsExtension().GetDateTimeNow(), DbType.DateTime);
		// 		await dbConnection.QueryAsync($"update vendors set is_assortment_updated = {isAssortimentUpdated} and last_assortiment_updated_date = @Date where id = " + '\u0027' + vendorId + '\u0027', dp);
		// 	}
		// }
		/* REMOVE */
		/* COUNT */
		public async Task<int> CountCommentsAsync(Guid vendorId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.ExecuteScalarAsync<int>("select count(pc.id) from product_comments pc inner join products pr on pc.productid = pr.id where pr.vendorid = " + '\u0027' + vendorId + '\u0027');
			}
		}
		public int CountCommentsById(Guid vendorId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return dbConnection.ExecuteScalar<int>(
					"select count(pc.id) from product_comments pc inner join products pr on pc.productid = pr.id where pr.vendorid = " + '\u0027' + vendorId + '\u0027');
			}
		}
	}
}
