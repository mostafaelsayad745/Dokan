

using Dokan.DataAccess.Data;
using Dokan.Models.Repositories;

namespace Dokan.DataAccess.ReposIplementaion
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DokanDbContext _context;

        // why we use this property here to access the category repository instead of creating an instance of it in the constructor and then use it in the complete method then i use the propty to access it from outside the class? example to access it from a service layer or a controller in an ASP.NET Core application.
        // Answer: By using a property, we can access the repository from outside the UnitOfWork class, allowing for better separation of concerns and easier testing.
        public ICategoryRepository Categories {  get; private set; }
        public UnitOfWork(DokanDbContext context)
        {
            _context = context;
            Categories = new CategoryRepository(_context);
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
