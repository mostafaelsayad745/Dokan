

using Dokan.Models.Models;

namespace Dokan.Models.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task UpdateCategoryAsync(Category category);

    }
}
