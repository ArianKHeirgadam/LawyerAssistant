using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using LawyerAssistant.Persistance.ApplicationDbContexts;

namespace LawyerAssistant.Persistance;

public class MainDBContextFactory : IDesignTimeDbContextFactory<MainDBContext>
{
    public MainDBContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<MainDBContext>(); 
        builder.UseSqlServer(configuration.GetSection("AppConfig:connectionString").Value); 
        return new MainDBContext(builder.Options);
    }
}
