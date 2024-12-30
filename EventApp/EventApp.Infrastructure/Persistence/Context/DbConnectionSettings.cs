using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EventApp.Infrastructure.Persistence.Context
{
    public class DbConnectionSettings : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("C:\\Users\\pussydestroyer\\source\\repos\\EventApp\\EventApp.API\\appsettings.json")
                    .Build();
       
            var connectionString = configuration.GetConnectionString("DefaultConnection");

                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseNpgsql(connectionString);

                return new ApplicationDbContext(optionsBuilder.Options);
            }
        
    }
}
