using System.ComponentModel.DataAnnotations;

namespace Dokan.Models.Models.BlogAndEngagement
{
    public class Blog
    {
        public Guid Id { get; set; }
        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        public string? Slug { get; set; } = string.Empty;
        [Required, MaxLength(600)]
        public string Content { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }


        public Blog()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }



    }
}
