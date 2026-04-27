using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dokan.Models.Models.ProductCatalog
{
    public class Category
    {
      

        public Guid Id { get; init; } 
        [Required,MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(150)]
        public required string Slug { get; set; } = string.Empty;
        public required string Description { get; set; } = string.Empty;
        public required string ImageUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // self-referencing relationship for subcategories
        public Guid? ParentCategoryId { get; set; }
        [ForeignKey("ParentCategoryId")]
        public Category? ParentCategory { get; set; }

        // navigation properties
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Category> SubCategories { get; set; } = new List<Category>();

        public Category()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Products = new List<Product>();
            SubCategories = new List<Category>();
        }
    }
}
