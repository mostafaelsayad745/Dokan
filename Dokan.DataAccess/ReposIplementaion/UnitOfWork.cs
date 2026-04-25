

using Dokan.DataAccess.Data;
using Dokan.Models.Repositories;

namespace Dokan.DataAccess.ReposIplementaion
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DokanDbContext _context;

        public ICategoryRepository Categories { get; private set; }
        public IProductRepository Products { get; private set; }
        public UnitOfWork(DokanDbContext context)               
        {
            _context = context;
            Categories = new CategoryRepository(_context);
            Products = new ProductRepository(_context);

        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            
            _context.Dispose();
        }
    }
}
