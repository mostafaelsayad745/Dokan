using Dokan.Models.Models.ProductCatalog;

namespace Dokan.Models.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task UpdateCategoryAsync(Category category);

    }
}
