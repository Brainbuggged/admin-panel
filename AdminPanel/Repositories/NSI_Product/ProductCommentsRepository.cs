using System;
using System.Collections.Generic;
using Npgsql;
using System.Data;
using System.Threading.Tasks;
using AdminPanel.Extensions;
using AdminPanel.Models.Catalog.GetProductCard.Response;
using AdminPanel.Models.Catalog.GetVendorCard.Response;
using Dapper;

namespace AdminPanel.Core.Repositories.NSI_Product
{
	public class ProductCommentsRepository
	{
		public string connectionString { get; set; }

		public ProductCommentsRepository()
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
		public async Task<IEnumerable<ResponseProductComment>> GetByProductAsync(Guid productId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QueryAsync<ResponseProductComment>(
					"select " +
					"cl.photo as client_photo, " +
					"cl.name as client_name, " +
					"pc.rating as comment_rating, " +
					"pc.header as comment_header, " +
					"pc.text as comment_text " +
					"from product_comments pc  " +
					"inner join products pr " +
					"on pc.productid = pr.id  " +
					"inner join clients cl " +
					"on pc.clientid = cl.id " +
					"where pc.productid = "
					+ '\u0027' + productId + '\u0027');
			}
		}
		public async Task<IEnumerable<ResponseVendorComment>> GetByVendorAsync(Guid vendorId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QueryAsync<ResponseVendorComment>(
					"select " +
					"cl.photo as client_photo, " +
					"cl.name as client_name, " +
					"pc.rating as comment_rating, " +
					"pc.header as comment_header, " +
					"pc.text as comment_text " +
					"from product_comments pc  " +
					"inner join products pr " +
					"on pc.productid = pr.id  " +
					"inner join clients cl " +
					"on pc.clientid = cl.id " +
					"where pr.vendorid = "
					+ '\u0027' + vendorId + '\u0027' + " limit 8");
			}
		}
		/* INSERT */
		/* UPDATE */
		/* REMOVE */
		/* COUNT */
	}
}
