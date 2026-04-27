using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dokan.Models.Models.ProductCatalog
{
    public class ProductImage
    {
        public Guid Id { get; init; }
        [Required] 
        public string ImgUrl { get; set; } = string.Empty;
        public bool IsMain { get; set; }  // true if this is the primary thumbnail image for the product
        public virtual Product? Product { get; set; }
        [ForeignKey(nameof(Product))]
        public required Guid ProductId { get; set; }

        public ProductImage()
        {
            Id = Guid.NewGuid();
        }
    }
}
