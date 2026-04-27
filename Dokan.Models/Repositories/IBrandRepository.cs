using Dokan.Models.Models.ProductCatalog;

namespace Dokan.Models.Repositories
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task UpdateBrandAsync(Brand brand);


    }
}
