using Dokan.Models.Models.BlogAndEngagement;
using Dokan.Models.Models.ShoppingCartAndOrders;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dokan.Models.Models.ProductCatalog
{
    public class Product
    {
        public Guid Id { get; init; }
        [Required , MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Slug { get; set; } = string.Empty;
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OldPrice { get; set; }

        [MaxLength(500)]
        public string ShortDescription { get; set; } = string.Empty;
        public string FullDescription { get; set; } = string.Empty;

        public string SKU { get; set; } = String.Empty;
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } 

        // navigation properties

        [DisplayName("Category")]
        public  Guid CategoryId { get; set; }
        public virtual Category? Category { get; set; }


        [DisplayName("Brand")]
        public  Guid BrandId { get; set; }
        [ForeignKey("BrandId")]
        public virtual Brand? Brand { get; set; }

        // Navigation property for related product images

        public virtual ICollection<ProductImage> ProductImages { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        // reviews

        public Product()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            ProductImages = new List<ProductImage>();
            OrderItems = new List<OrderItem>();
            Reviews = new List<Review>();
        }

    }
}
