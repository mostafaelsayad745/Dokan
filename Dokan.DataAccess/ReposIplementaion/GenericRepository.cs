
using Dokan.DataAccess.Data;
using Dokan.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dokan.DataAccess.ReposIplementaion
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly DokanDbContext _context;
        DbSet<T> _dbset;

        public GenericRepository(DokanDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _dbset.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? perdicate = null, string? includeWords = null)
        {
            IQueryable<T> query = _dbset;
            if (perdicate is not null)
            {
                query = query.Where(perdicate);
            }
            if (includeWords is not null)
            {
                foreach (var includeWord in includeWords.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeWord);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetFristOrDefaultAsync(Expression<Func<T, bool>>? perdicate = null, string? includeWords = null)
        {
            IQueryable<T> query = _dbset;
            if (perdicate is not null)
            {
                query = query.Where(perdicate);
            }
            if (includeWords is not null)
            {
                foreach (var includeWord in includeWords.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeWord);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public  void Remove(T entity)
        {
             _dbset.Remove(entity);
        }

        public  void RemoveRange(IEnumerable<T> entities)
        {
            _dbset.RemoveRange(entities);
        }
    }
}
