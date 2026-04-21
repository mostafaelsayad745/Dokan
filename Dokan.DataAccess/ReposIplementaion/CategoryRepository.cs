
using Dokan.DataAccess.Data;
using Dokan.Models.Models;
using Dokan.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dokan.DataAccess.ReposIplementaion
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly DokanDbContext _context;

        public CategoryRepository(DokanDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var categoryToUpdate = await _context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            if (categoryToUpdate != null) {
                categoryToUpdate.Name = category.Name;
                categoryToUpdate.Description = category.Description;
                categoryToUpdate.CreatedAt = DateTime.Now;
            }
        }
    }
}
