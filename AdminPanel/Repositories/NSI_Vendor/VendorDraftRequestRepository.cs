using Dapper;
using Npgsql;
using AdminPanel.Models.Models.NSI_Vendor;
using System.Data;
using System.Threading.Tasks;
using AdminPanel.Extensions;

namespace AdminPanel.Core.Repositories.NSI_Vendor
{
	public class VendorDraftRequestRepository
	{
		public string connectionString { get; set; }

		public VendorDraftRequestRepository()
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

		public async Task AddAsync(VendorDraftRequestModel request)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _request = "insert into vendor_draft_requests(id, message, category, request_date, closed_date, vendorid, number) VALUES(@id, @message, @category, @request_date, @closed_date, @vendorid, @number)";
				await dbConnection.QueryAsync(_request, request);
			}
		}
		public async Task<int> CountRequestsAsync()
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.ExecuteScalarAsync<int>("select count(id) from vendor_draft_requests");
			}
		}
		public async Task<VendorDraftRequestModel> GetByNumberAsync(int requestNumber)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QuerySingleOrDefaultAsync<VendorDraftRequestModel>("select * from vendor_draft_requests where number = " + '\u0027' + requestNumber + '\u0027');
			}
		}
		public async Task CloseAsync(int requestNumber)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _request = "update vendor_draft_requests set closed_date = @Date where number = " + '\u0027' + requestNumber + '\u0027';
				DynamicParameters dp = new DynamicParameters();
				dp.Add("@Date", new SettingsExtension().GetDateTimeNow(), DbType.DateTime);
				await dbConnection.QueryAsync(_request, dp);
			}
		}
	}
}
