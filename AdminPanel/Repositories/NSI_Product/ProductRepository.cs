using Dapper;
using Npgsql;
using AdminPanel.Models;
using AdminPanel.Models.Models.NSI_Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AdminPanel.Extensions;
using AdminPanel.Models.Catalog.GetVendorCard.Response;
using AdminPanel.ViewModels.Product.GetDraftProduct.Response;
using AdminPanel.ViewModels.Product.GetMyProducts.Response;
using AdminPanel.ViewModels.Product.UpdateProduct.Query;

namespace AdminPanel.Core.Repositories.NSI_Product
{
	public class ProductRepository
	{
		public string connectionString { get; set; }
		public ProductRepository()
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
		public async Task<ProductModel> GetByNumberAsync(string productNumber)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QuerySingleOrDefaultAsync<ProductModel>("select * from products where number = " + '\u0027' + productNumber + '\u0027');
			}
		}

		public async Task<ProductModel> GetByIdAsync(Guid productId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QuerySingleOrDefaultAsync<ProductModel>("select * from products where id = " + '\u0027' + productId + '\u0027');
			}
		}

		public async Task<int> CountAsync(Guid productId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.ExecuteScalarAsync<int>("select count from products where id = " + '\u0027' + productId + '\u0027');
			}
		}
		public IEnumerable<string> GetBrandList(string categoryName)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return dbConnection.Query<string>("select distinct brand from products pr join product_categories pc on pr.categoryid = pc.id where en_name = " + '\u0027' + categoryName + '\u0027');
			}
		}
		public IEnumerable<string> GetCityList(string categoryName)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return dbConnection.Query<string>("select distinct city from products pr join product_categories pc on pr.categoryid = pc.id where en_name = " + '\u0027' + categoryName + '\u0027');
			}
		}
		public async Task<IEnumerable<ProductForReturn>> GetCatalogProductsWithParametersAsync(Guid clientId, CreatedFilters _requestParams)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var returner = new List<ProductForReturn>();
				var _request = "select pr.publication_date, "
						+ "pr.name, "
						+ "pr.id, "
						+ "pr.number, "
						+ "pr.city, "
						+ "pr.country, "
						+ "pr.release_year, "
						+ "pr.type, "
						+ "pr.brand, "
						+ "pr.rating, "
						+ "pr.prise, "
						+ "Pr.id, "
						+ "pr.vendorid, "
						+ "pc.ru_name, "
						+ "pr.is_in_cart, "
						+ "pr.is_in_favourite " +
					" from ((select pr1.publication_date, "
						+ "pr1.name, "
						+ "pr1.number, "
						+ "pr1.city, "
						+ "pr1.country, "
						+ "pr1.release_year, "
						+ "pr1.type, "
						+ "pr1.brand, "
						+ "pr1.rating, "
						+ "pr1.prise, "
						+ "pr1.id, "
						+ "pr1.vendorid, "
						+ "pr1.count, "
						+ "(case when ic.in_cart = true then true else false end) as is_in_cart, "
						+ "(case when if.in_favourite = true then true else false end) as is_in_favourite, "
						+ "pr1.categoryid from ( ";
		
				_request += _requestParams.Properties;
				_request +=
		
							" left join"
						+ " (select pr.id, true as in_cart from products pr where pr.id in (select productid from cart_items where clientid = "
						+ '\u0027' + clientId + '\u0027' + " )) ic"
						+" on pr1.id = ic.id" +
						" left join"
						+ " (select pr.id, true as in_favourite from products pr where pr.id in (select productid from favourite_products where clientid = "
						+ '\u0027' + clientId + '\u0027' + " )) if"
						+ " on pr1.id = if.id"
						+ " ) ) pr,";
				_request += _requestParams.Category;
		
				if (_requestParams.Type != "")
					_request += " and " + _requestParams.Type;
		
				_request += " and " + _requestParams.Amount;
		
				if (_requestParams.Prise != "")
					_request += " and (" + _requestParams.Prise + " )";
		
				if (_requestParams.Year != "")
					_request += " and (" + _requestParams.Year + " )";
		
				if (_requestParams.Brand != "")
					_request += " and " + _requestParams.Brand;
		
				if (_requestParams.City != "")
					_request += " and " + _requestParams.City;
		
				if (_requestParams.Search != "")
					_request += " and " + _requestParams.Search;
		
				_request += " " + _requestParams.Sorting;
		
				return await dbConnection.QueryAsync<ProductForReturn>(_request);
			}
		}
		
		
		public async Task<IEnumerable<ProductForReturn>> GetCatalogProductsWithOutParametersAsync(Guid clientId, CreatedFilters _requestParams)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var returner = new List<ProductForReturn>();
				var _request = "select pr.publication_date, "
						+ "pr.name, "
						+ "pr.id, "
						+ "pr.number, "
						+ "pr.city, "
						+ "pr.country, "
						+ "pr.release_year, "
						+ "pr.type, "
						+ "pr.brand, "
						+ "pr.rating, "
						+ "pr.prise, "
						+ "pr.id, "
						+ "pr.vendorid, "
						+ "pc.ru_name, "
						+ "pr.is_in_cart, "
						+ "pr.is_in_favourite " +
					" from (select pr1.publication_date, "
						+ "pr1.name, "
						+ "pr1.number, "
						+ "pr1.city, "
						+ "pr1.country, "
						+ "pr1.release_year, "
						+ "pr1.type, "
						+ "pr1.brand, "
						+ "pr1.rating, "
						+ "pr1.prise, "
						+ "pr1.vendorid, "
						+ "pr1.id, "
						+ "pr1.count, "
						+ "(case when ic.in_cart = true then true else false end) as is_in_cart, "
						+ "(case when if.in_favourite = true then true else false end) as is_in_favourite, "
						+ "pr1.categoryid from ";
		
				_request += "products pr1 "
							+ " left join"
							+ " (select pr.id, true as in_cart from products pr where pr.id in (select productid from cart_items where clientid = "
							+ '\u0027' + clientId + '\u0027' + " )) ic"
							+ " on pr1.id = ic.id" +
							" left join"
							+ " (select pr.id, true as in_favourite from products pr where pr.id in (select productid from favourite_products where clientid = "
							+ '\u0027' + clientId + '\u0027' + " )) if"
							+ " on pr1.id = if.id "
							+ $"where pr1.status in ({new SettingsExtension().AvailableProductStatuses()})) pr, ";
				_request += _requestParams.Category;
		
				if (_requestParams.Type != "")
					_request += " and " + _requestParams.Type;
		
				_request += " and " + _requestParams.Amount;
		
				if (_requestParams.Prise != "")
					_request += " and (" + _requestParams.Prise + " )";
		
				if (_requestParams.Year != "")
					_request += " and (" + _requestParams.Year + " )";
		
				if (_requestParams.Brand != "")
					_request += " and " + _requestParams.Brand;
		
				if (_requestParams.City != "")
					_request += " and " + _requestParams.City;
		
				if (_requestParams.Search != "")
					_request += " and " + _requestParams.Search;
		
				_request += " " + _requestParams.Sorting;
		
				return await dbConnection.QueryAsync<ProductForReturn>(_request);
			}
		}
		public async Task<IEnumerable<ResponseVendorProduct>> GetProductsByVendorAsync(Guid clientId, Guid vendorId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _request = "select " +
				"pr.id as product_id, " +
				"pr.publication_date as product_publish_date, " +
				"pr.name as product_name, " +
				"pr.number as product_number, " +
				"pr.city as product_city, " +
				"pr.country as product_country, " +
				"pr.type as product_type, " +
				"pr.brand as product_brand, " +
				"pr.rating as product_rating, " +
				"pr.prise as product_prise, " +
				"pr.ru_name as product_category, " +
				"(case when pr.in_cart = true then true else false end) as is_in_cart,  " +
				"(case when pr.in_favourite = true then true else false end) as is_in_favourite, " +
				"pp.linq as product_photo " +
				"from " +
				"(select " +
				"pr.id, " +
				"pr.publication_date, " +
				"pr.name, " +
				"pr.country, " +
				"pr.city, " +
				"pr.brand, " +
				"pr.prise, " +
				"pr.number, " +
				"pr.type, " +
				"pr.rating, " +
				"sq.in_cart, " +
				"if.in_favourite, " +
				"pc.ru_name " +
				"from products pr " +
		
		
				"left join " +
				"(select pr.id, true as in_cart from products pr where pr.id in " +
				"(select productid from cart_items where clientid = "
				+ '\u0027' + clientId + '\u0027' +
				")) sq " +
					"on pr.id = sq.id " +
		
				"left join " +
				"(select pr.id, true as in_favourite from products pr where pr.id in " +
				"(select productid from favourite_products where clientid = "
				+ '\u0027' + clientId + '\u0027' +
				")) if " +
					"on pr.id = if.id " +
		
		
					"join product_categories pc " +
					"on pr.categoryid = pc.id " +
					"where pr.vendorid = "
					+ '\u0027' + vendorId + '\u0027' +
					$"and pr.status in ({new SettingsExtension().AvailableProductStatuses()})) pr " +
					"left join " +
					"(select productid, linq from product_photoes pp where pp.is_main = true) pp " +
					$"on pp.productid = pr.id";
		
				return await dbConnection.QueryAsync<ResponseVendorProduct>(_request);
			}
		}
		public async Task<ResponseProductToUpdate> GetDraftProsuctAsync(int productNumber)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _request = "select " +
				"pr.type as product_type, " +
				"pr.name as product_name, " +
				"pr.ru_name as product_category, " +
				"pr.brand as product_brand, " +
				"pr.prise as product_prise, " +
				"pr.description as product_description, " +
				"pr.release_year as product_release_year, " +
				"pr.count as product_amount, " +
				"pr.is_pickuped as product_is_pickuped, " +
				"pr.address as product_address, " +
				"pr.country as product_country, " +
				"pr.city as product_city, " +
				"pr.x as product_x, " +
				"pr.y as product_y, " +
				"pr.is_delivered as product_is_delivered " +
				"from " +
				"(select " +
				"pr.type, " +
				"pr.name, " +
				"pc.ru_name, " +
				"pr.brand, " +
				"pr.prise, " +
				"pr.description, " +
				"pr.release_year, " +
				"pr.count, " +
				"pr.is_pickuped, " +
				"pr.address, " +
				"pr.country, " +
				"pr.city, " +
				"pr.x, " +
				"pr.y, " +
				"pr.is_delivered " +
				"from products pr " +
				"join product_categories pc " +
				"on pr.categoryid = pc.id " +
				"where pr.number = "
				+ '\u0027' + productNumber + '\u0027' +
				"and pr.status = 3 " +
				") pr";
				return await dbConnection.QuerySingleOrDefaultAsync<ResponseProductToUpdate>(_request);
			}
		}


		public async Task<ProductModel> CheckDraftAsync(Guid vendorId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _request = $"select number, id from products where status = {(int)ProductStatus.Chernovik} and vendorid = " + '\u0027' + vendorId + '\u0027';
				return await dbConnection.QueryFirstOrDefaultAsync<ProductModel>(_request);
			}
		}
		public async Task<ProductModel> GetByNumberAndVendorAsync(Guid productId, Guid vendorId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.QuerySingleOrDefaultAsync<ProductModel>($"select number, id, status, is_delivery_expected, count from products where id = " + '\u0027' + productId + '\u0027' + " and vendorid = " + '\u0027' + vendorId + '\u0027');
			}
		}
		public async Task<IEnumerable<ResponseMyProduct>> GetMyProductsAsync(Guid vendorId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _request = "select " +
				"pr.publication_date as product_publish_date, " +
				"pr.name as product_name, " +
				"pr.number as product_number, " +
				"pr.city as product_city, " +
				"pr.country as product_country, " +
				"pr.type as product_type, " +
				"pr.id as product_id, " +
				"pr.brand as product_brand, " +
				"pr.rating as product_rating, " +
				"pr.prise as product_prise, " +
				"pr.ru_name as product_category, " +
				"pr.status as product_status, " +
				"pr.count as product_count, " +
				"pr.is_delivery_expected as product_is_delivery_expected, " +
				"pp.linq as product_photo " +
				"from " +
				"(select " +
				"pr.id, " +
				"pr.publication_date, " +
				"pr.name, " +
				"pr.number, " +
				"pr.city, " +
				"pr.country, " +
				"pr.type, " +
				"pr.brand, " +
				"pr.rating, " +
				"pr.prise, " +
				"pc.ru_name, " +
				"pr.status, " +
				"pr.count, " +
				"pr.is_delivery_expected " +
				"from products pr " +
				"join product_categories pc " +
				"on pr.categoryid = pc.id " +
				"where pr.vendorid = "
				+ '\u0027' + vendorId + '\u0027' +
				$"and pr.status in ({(int)ProductStatus.OjidaetProverki}, {(int)ProductStatus.Podtverzhden}, {(int)ProductStatus.Snyat}, {new SettingsExtension().AvailableProductStatuses()}) " +
				") pr " +
				"left join " +
				"(select productid, linq from product_photoes pp where pp.is_main = true) pp " +
				"on pp.productid = pr.id";
				return await dbConnection.QueryAsync<ResponseMyProduct>(_request);
			}
		}
		public async Task<IEnumerable<ResponseMyProduct>> GetMyDraftProductsAsync(Guid vendorId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _request = "select " +
				"pr.publication_date as product_publish_date, " +
				"pr.name as product_name, " +
				"pr.number as product_number, " +
				"pr.city as product_city, " +
				"pr.country as product_country, " +
				"pr.type as product_type, " +
				"pr.brand as product_brand, " +
				"pr.id as product_id, " +
				"pr.rating as product_rating, " +
				"pr.prise as product_prise, " +
				"pr.ru_name as product_category, " +
				"pr.status as product_status, " +
				"pr.count as product_count, " +
				"pr.is_delivery_expected as product_is_delivery_expected, " +
				"pp.linq as product_photo " +
				"from " +
				"(select " +
				"pr.id, " +
				"pr.publication_date, " +
				"pr.name, " +
				"pr.number, " +
				"pr.city, " +
				"pr.country, " +
				"pr.type, " +
				"pr.brand, " +
				"pr.rating, " +
				"pr.prise, " +
				"pc.ru_name, " +
				"pr.status, " +
				"pr.count, " +
				"pr.is_delivery_expected " +
				"from products pr " +
				"join product_categories pc " +
				"on pr.categoryid = pc.id " +
				"where pr.vendorid = "
				+ '\u0027' + vendorId + '\u0027' +
				$"and pr.status in ({(int)ProductStatus.Chernovik}) " +
				") pr " +
				"left join " +
				"(select productid, linq from product_photoes pp where pp.is_main = true) pp " +
				"on pp.productid = pr.id";
				return await dbConnection.QueryAsync<ResponseMyProduct>(_request);
			}
		}
		public async Task<IEnumerable<ResponseMyProduct>> GetMyArchiveProductsAsync(Guid vendorId)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _request = "select " +
				"pr.publication_date as product_publish_date, " +
				"pr.name as product_name, " +
				"pr.number as product_number, " +
				"pr.city as product_city, " +
				"pr.id as product_id, " +
				"pr.country as product_country, " +
				"pr.type as product_type, " +
				"pr.brand as product_brand, " +
				"pr.rating as product_rating, " +
				"pr.prise as product_prise, " +
				"pr.ru_name as product_category, " +
				"pr.status as product_status, " +
				"pr.count as product_count, " +
				"pr.is_delivery_expected as product_is_delivery_expected, " +
				"pp.linq as product_photo " +
				"from " +
				"(select " +
				"pr.id, " +
				"pr.publication_date, " +
				"pr.name, " +
				"pr.number, " +
				"pr.city, " +
				"pr.country, " +
				"pr.type, " +
				"pr.brand, " +
				"pr.rating, " +
				"pr.prise, " +
				"pc.ru_name, " +
				"pr.status, " +
				"pr.count, " +
				"pr.is_delivery_expected " +
				"from products pr " +
				"join product_categories pc " +
				"on pr.categoryid = pc.id " +
				"where pr.vendorid = "
				+ '\u0027' + vendorId + '\u0027' +
				$"and pr.status in ({(int)ProductStatus.Archive}) " +
				") pr " +
				"left join " +
				"(select productid, linq from product_photoes pp where pp.is_main = true) pp " +
				"on pp.productid = pr.id";
				return await dbConnection.QueryAsync<ResponseMyProduct>(_request);
			}
		}
		/* ADD */
		public async Task AddAsync(ProductModel newProduct)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _request =
					@"insert into products
					(
					id, 
					number, 
					publication_date,
					name,
					country,
					city,
					release_year,
					type,
					brand,
					prise,
					count,
					is_delivery_expected,
					description,
					address,
					rating,
					status,
					is_pickuped,
					is_delivered,
					x,
					y,
					categoryid,
					vendorid,
					last_status_update
					) VALUES(
					@id, 
					@number, 
					@publication_date, 
					@name, 
					@country, 
					@city, 
					@release_year, 
					@type, 
					@brand, 
					@prise, 
					@count, 
					@is_delivery_expected, 
					@description, 
					@address, 
					@rating, 
					@status, 
					@is_pickuped,
					@is_delivered,
					@x,
					@y,
					@categoryid,
					@vendorid,
					@last_status_update)";
				await dbConnection.QueryAsync(_request, newProduct);
			}
		}
		// /* UPDATE */
		public async Task ChangeProductStatusAsync(Guid productId, ProductStatus status)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				DynamicParameters dp = new DynamicParameters();
				dp.Add("@Date", new SettingsExtension().GetDateTimeNow(), DbType.DateTime);
				await dbConnection.QueryAsync($"update products set status = {(int)status}, last_status_update = @Date where id = " + '\u0027' + productId + '\u0027', dp);
			}
		}
		public async Task ChangeProductStatusAsync(int productNumber, ProductStatus status)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				DynamicParameters dp = new DynamicParameters();
				dp.Add("@Date", new SettingsExtension().GetDateTimeNow(), DbType.DateTime);
				await dbConnection.QueryAsync($"update products set status = {(int)status}, last_status_update = @Date where number = " + '\u0027' + productNumber + '\u0027', dp);
			}
		}
		public async Task ChangeProductCountAsync(Guid productId, int productCount)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync($"update products set count = {productCount} where id = " + '\u0027' + productId + '\u0027');
			}
		}
		public async Task ChangeProductDeliveryAsync(Guid productId, bool isExpected)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync($"update products set is_delivery_expected = {isExpected} where id = " + '\u0027' + productId + '\u0027');
			}
		}
		public async Task UpdateAsync(updateProduct updateProduct)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				var _request = "update products set " +
					"name = " + '\u0027' + updateProduct.product_name + '\u0027' + ", " +
					"number = " + '\u0027' + new TranslitExtension().MakeName(updateProduct.product_name) + '\u0027' + ", " +
					"categoryid = " + '\u0027' + updateProduct.product_category + '\u0027' + ", " +
					$"type = {updateProduct.product_type},  " +
					"brand = " + '\u0027' + updateProduct.product_brand + '\u0027' + ", " +
					"prise = @Prise, " +
					"description = " + '\u0027' + updateProduct.product_description + '\u0027' + ", " +
					$"release_year = {updateProduct.product_release_year},  " +
					$"count = {updateProduct.product_amount},  " +
					$"is_pickuped = {updateProduct.product_is_pickuped},  " +
					"address = " + '\u0027' + updateProduct.product_address + '\u0027' + ", " +
					"country = " + '\u0027' + updateProduct.product_country + '\u0027' + ", " +
					"city = " + '\u0027' + updateProduct.product_city + '\u0027' + ", " +
					"x = @X, " +
					"y = @Y, " +
					$"is_delivered = {updateProduct.product_is_delivered} " +
					$"where id = {updateProduct.product_id}";
				DynamicParameters dp = new DynamicParameters();
				dp.Add("@X", updateProduct.product_x, DbType.Double);
				dp.Add("@Y", updateProduct.product_y, DbType.Double);
				dp.Add("@Prise", updateProduct.product_prise, DbType.Double);
				await dbConnection.QueryAsync(_request, dp);
			}
		}
		public async Task ChangeCountByNumberAsync(Guid productId, int productCount)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync($"update products set count = {productCount} where id = " + '\u0027' + productId + '\u0027');
			}
		}
		/* REMOVE */
		public async Task RemoveByNumberAsync(int productNumber)
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				await dbConnection.QueryAsync("delete from products where number = " + productNumber);
			}
		}
		/* COUNT */
		public async Task<int> CountProductsAsync()
		{
			using (IDbConnection dbConnection = Connection)
			{
				dbConnection.Open();
				return await dbConnection.ExecuteScalarAsync<int>("select count (id) from products");
			}
		}

	}
}
