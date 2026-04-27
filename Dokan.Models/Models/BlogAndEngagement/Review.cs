
using Dokan.Models.Models.ProductCatalog;
using Dokan.Models.Models.UserMangement;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.ObjectiveC;

namespace Dokan.Models.Models.BlogAndEngagement
{
    public class Review
    {
        public Guid Id { get; set; }
        [Range(1,5)]
        public  int?  Rating { get; set; }
        public  string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } 


        public required Guid ProductId { get; set; }
        public  string? UserId { get; set; }

        // navigation properties
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }

        public Review()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
    }

}
