using Microsoft.EntityFrameworkCore;
using Amazon.Models;

namespace Amazon.Services
{
    public class ApplicationDBContex : DbContext
    {
        public ApplicationDBContex(DbContextOptions<ApplicationDBContex> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        // OnConfiguring is optional, as it's already configured in the Program.cs file
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("your-connection-string-here");  // Optional if already configured in Program.cs
            }
        }
    }
}
