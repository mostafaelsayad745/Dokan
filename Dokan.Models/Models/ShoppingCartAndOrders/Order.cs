using Dokan.Models.Models.UserMangement;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dokan.Models.Models.ShoppingCartAndOrders
{
    public class Order
    {
        public Guid Id { get; init; }
        public DateTime OrderDate { get; set; } 
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount => OrderItems?.Sum(x => x.Quantity * x.Product?.Price)??0;

        public OrderStatus OrderStatus { get; set; } 
        public PaymentStatus PaymentStatus { get; set; } 
        [Required , MaxLength(500)]
        public required string ShippingAddress { get; set; } = string.Empty;
        [MaxLength(500)]
        public required string BillingAddress { get; set; } = string.Empty;

        // navigation properties

        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser? ApplicationUser { get; set; }

        public virtual ICollection<OrderItem>  OrderItems { get; set; }

        public Order()
        {
            Id = Guid.NewGuid();
            OrderDate = DateTime.Now;
            OrderStatus = OrderStatus.Pending;
            PaymentStatus = PaymentStatus.Pending;
            OrderItems = new List<OrderItem>();
        }
    }
}
