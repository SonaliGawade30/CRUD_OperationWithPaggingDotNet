using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Amazon.Services
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDBContex>
    {
        public ApplicationDBContex CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContex>();

            // Load configuration from appsettings.json (this is needed at design-time)
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Use the connection string from the configuration
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDBContex(optionsBuilder.Options);
        }
    }
}

