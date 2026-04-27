using Dokan.DataAccess.Data;
using Dokan.Models.Models.ProductCatalog;
using Dokan.Models.Repositories;

namespace Dokan.DataAccess.ReposIplementaion
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        private readonly DokanDbContext _context;

        public BrandRepository(DokanDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task UpdateBrandAsync(Brand brand)
        {
            var brandToUpdate = _context.Brands.FirstOrDefault(x => x.Id == brand.Id);
            if (brandToUpdate != null)
            {
                brandToUpdate.Name = brand.Name;
                brandToUpdate.Slug = brand.Slug;
                brandToUpdate.LogoUrl = brand.LogoUrl;

            }

        }
    }
}
