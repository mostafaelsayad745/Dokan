
using Dokan.Models.Models.BlogAndEngagement;
using Dokan.Models.Models.ShoppingCartAndOrders;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Dokan.Models.Models.UserMangement
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
  
        [MaxLength(200)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;

        [MaxLength(20)]
        public string PostalCode { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } 


        // navigation properites 

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }


        public ApplicationUser()
        {
            CreatedAt = DateTime.UtcNow;
            Orders = new List<Order>();
            CartItems = new List<CartItem>();
            Reviews = new List<Review>();
        }
    }
}
