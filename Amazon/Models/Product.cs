using System.ComponentModel.DataAnnotations;
namespace Amazon.Models
{
    public class Product
    {
            [Key]
            public int? ProductId { get; set; }

            [MaxLength(100)]
            public string? ProductName { get; set; } = " ";

            public int? CategoryId { get; set; }  // Foreign key to Category

            public Category? Category { get; set; }  // Navigation property to Category

             public string CategoryName { get; set; }

    }
}
