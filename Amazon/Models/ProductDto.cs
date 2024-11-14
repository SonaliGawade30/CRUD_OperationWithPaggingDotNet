using Amazon.Models;
using System.ComponentModel.DataAnnotations;

namespace Amazon.Models
{
    public class ProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
        public int? ProducId { get; set; }
        public string CategoryName { get; set; }
    }
}
