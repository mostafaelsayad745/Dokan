using Dokan.Models.Models.ProductCatalog;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dokan.Models.Models.ShoppingCartAndOrders
{
    public class OrderItem
    {
        public Guid Id { get; init; }
        public required int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public required decimal  UnitPrice { get; set; }

        // navigation properties
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }


        public Guid OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }

        public OrderItem()
        {
            Id = Guid.NewGuid();

        }

    }
}
