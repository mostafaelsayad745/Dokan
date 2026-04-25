

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dokan.Models.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [DisplayName("Image")]
        [ValidateNever]
        public string Img { get; set; } = string.Empty;
        [Required]

        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        [DisplayName("Category")]
        public Guid CategoryId { get; set; }
        [ValidateNever]
        public Category? Category { get; set; }
    }
}
