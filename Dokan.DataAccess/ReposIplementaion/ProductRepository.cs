
using Dokan.DataAccess.Data;
using Dokan.Models.Models.ProductCatalog;
using Dokan.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dokan.DataAccess.ReposIplementaion
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DokanDbContext _context;

        public ProductRepository(DokanDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task UpdateProductAsync(Product product)
        {
            var productToUpdate = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
            if (productToUpdate != null) {
                productToUpdate.Name = product.Name;
                productToUpdate.FullDescription = product.FullDescription;
                productToUpdate.SKU= product.SKU;
                productToUpdate.BrandId = product.BrandId;
                productToUpdate.CreatedAt = DateTime.UtcNow;
                productToUpdate.StockQuantity = product.StockQuantity;
                productToUpdate.IsActive = product.IsActive;
                productToUpdate.Slug= product.Slug;
                productToUpdate.OldPrice = product.OldPrice;
                productToUpdate.Price = product.Price;
                productToUpdate.ProductImages = product.ProductImages;
                productToUpdate.CategoryId = product.CategoryId;
            }
        }
    }
}
