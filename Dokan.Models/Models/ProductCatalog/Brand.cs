

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Dokan.Models.Models.ProductCatalog
{
    public class Brand
    {
        public Guid Id { get; init; }
        [Required]
        public required string Name { get; set; } = string.Empty;
        public required string Slug { get; set; } = string.Empty;
        [ValidateNever]
        public string? LogoUrl { get; set; }


        // Navigation property for related products
        public ICollection<Product> Products { get; set; }

        public Brand()
        {
            Id = Guid.NewGuid();
            Products = new List<Product>();
        }
    }
}
