using System.ComponentModel.DataAnnotations;

namespace Dokan.Models.Models.BlogAndEngagement
{
    public class Contactmessage
    {
        public Guid Id { get; init; }
        [Required, MaxLength(100)]
        public required string Name { get; set; }
        [Required, MaxLength(100) , EmailAddress]
        public required string Email { get; set; }
        [MaxLength(200)]
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }


        public Contactmessage()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

    }

}
