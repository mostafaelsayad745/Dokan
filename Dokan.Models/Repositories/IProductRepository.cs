using Dokan.Models.Models.ProductCatalog;

namespace Dokan.Models.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task UpdateProductAsync(Product product);

    }
}
