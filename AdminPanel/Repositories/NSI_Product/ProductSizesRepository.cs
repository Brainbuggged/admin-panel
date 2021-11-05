using Dapper;
using Npgsql;
using System.Data;
using System.Threading.Tasks;
using AdminPanel.Extensions;

namespace AdminPanel.Core.Repositories.NSI_Product
{
	public class ProductSizesRepository
	{
		public string connectionString { get; set; }

		public ProductSizesRepository()
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
		public async Task<string> GetDefaultAsync()
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QueryFirstOrDefaultAsync<string>("select linq from product_sizes");
			}
		}
		/* INSERT */
		/* UPDATE */
		/* REMOVE */
		/* COUNT */
	}
}
