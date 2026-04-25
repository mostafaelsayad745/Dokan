

using Dokan.Models.Models;

namespace Dokan.Models.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task UpdateProductAsync(Product product);

    }
}
