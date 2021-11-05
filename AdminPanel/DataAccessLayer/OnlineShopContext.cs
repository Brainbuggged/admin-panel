using AdminPanel.Models.Models.NSI_Client;
using AdminPanel.Models.Models.NSI_Logger;
using AdminPanel.Models.Models.NSI_Order;
using AdminPanel.Models.Models.NSI_Product;
using AdminPanel.Models.Models.NSI_Vendor;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.DataAccessLayer
{
    public class OnlineShopContext : DbContext
    {
        public OnlineShopContext(DbContextOptions options) : base(options) { }
        public DbSet<CartItemModel> CartItemModels { get; set; }

        protected override void OnModelCreating(ModelBuilder _modelBuilder)
        {
            
        }
        public DbSet<ClientModel> clients { get; set; }
        public DbSet<CartItemModel> cart_items { get; set; }
        public DbSet<FavouriteProductModel> favourite_products { get; set; }
        public DbSet<FavouriteVendorModel> favourite_vendors { get; set; }
        public DbSet<ChatMessageModel> chat_messages { get; set; }
        ///////////////////////////////////////////////////////////////////////
        public DbSet<OrderModel> orders { get; set; }
        public DbSet<OrderProductModel> order_products { get; set; }
        public DbSet<OrderStatusChangeModel> order_status_changes { get; set; }
        public DbSet<ConflictMessageModel> conflict_messages { get; set; }
        ///////////////////////////////////////////////////////////////////////
        public DbSet<ProductCategoryModel> product_categories { get; set; }
        public DbSet<ProductModel> products { get; set; }
        public DbSet<ProductPhotoModel> product_photoes { get; set; }
        public DbSet<ProductPropertyModel> product_properties { get; set; }
        public DbSet<ProductCommentModel> product_comments { get; set; }
        public DbSet<ProductSizesModel> product_sizes { get; set; }
        ///////////////////////////////////////////////////////////////////////
        public DbSet<VendorModel> vendors { get; set; }
        public DbSet<VendorSocialModel> vendor_socials { get; set; }
        public DbSet<VendorDraftRequestModel> vendor_draft_requests { get; set; }
        ///////////////////////////////////////////////////////////////////////
        public DbSet<WebLogModel> web_logs { get; set; }
    }
}