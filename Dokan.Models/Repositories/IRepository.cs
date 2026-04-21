

using System.Linq.Expressions;

namespace Dokan.Models.Repositories
{
    public interface IRepository <T> where T : class
    {
        // _context.categories.Where(c => c.Id == id).Include(c => c.Products).FirstOrDefaultAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>>? perdicate = null , string? includeWords = null);
        Task<T> GetFristOrDefaultAsync(Expression<Func<T, bool>>? perdicate = null, string? includeWords = null);
        Task AddAsync(T entity);
        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);


    }
}
