

namespace Dokan.Models.Models
{
    public class Category
    {
        public Category(string name, string discription)
        {
            Name = name;
            Discription = discription;
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Discription { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
