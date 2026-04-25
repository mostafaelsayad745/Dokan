

using Dokan.Models.Models;
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
    }
}
