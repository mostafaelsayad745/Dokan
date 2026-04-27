using Dokan.Models.Models.BlogAndEngagement;
using Dokan.Models.Models.ProductCatalog;
using Dokan.Models.Models.ShoppingCartAndOrders;
using Microsoft.EntityFrameworkCore;


namespace Dokan.DataAccess.Data
{
    public class DokanDbContext : DbContext
    {
        public DokanDbContext(DbContextOptions<DokanDbContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Contactmessage> Contactmessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>()
                .HasMany(c => c.SubCategories)
                .WithOne(c => c.ParentCategory)
                .HasForeignKey(c =>c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
