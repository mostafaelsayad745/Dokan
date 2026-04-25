
using Dokan.DataAccess.Data;
using Dokan.Models.Models;
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
                productToUpdate.Description = product.Description;
                productToUpdate.Price = product.Price;
                productToUpdate.Img = product.Img;
                productToUpdate.CategoryId = product.CategoryId;
            }
        }
    }
}
