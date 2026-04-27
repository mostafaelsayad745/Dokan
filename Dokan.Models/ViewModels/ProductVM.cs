using Dokan.Models.Models.ProductCatalog;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dokan.Models.ViewModels
{
    public class ProductVM
    {
        public Product? Product { get; set; }
        public IEnumerable<SelectListItem>? CategoryList { get; set; }
        public IEnumerable<SelectListItem>? BrandList { get; set; }
    }
}
