using System.ComponentModel.DataAnnotations;

namespace Amazon.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [MaxLength(100)]
        public string CategoryName { get; set; } = string.Empty;

        // Navigation property for related products
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

