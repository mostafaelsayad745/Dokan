using Dokan.Models.Models.ProductCatalog;
using Dokan.Models.Models.UserMangement;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dokan.Models.Models.ShoppingCartAndOrders
{
    public class CartItem
    {
        public Guid Id { get; init; }
        public string SessionId { get; set; } = string.Empty;
        [Range(0, int.MaxValue)]
        public int Quanity { get; set; }
        public DateTime CreatedAt { get; set; }

        // nav properties 

        public required Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }


        public string? ApplicationUserId { get; set; } // null if guest
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser? ApplicationUser { get; set; }

        public CartItem()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
    }
}
