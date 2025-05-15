using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Persistance.ApplicationDbContexts;
using LawyerAssistant.Persistance.Contents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace LawyerAssistant.Persistance;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddTransient<IMainDBContext, MainDBContext>();

        services.AddDbContext<MainDBContext>(options =>
           options.UseSqlServer(configuration.GetSection("AppConfig:connectionString").Value),  ServiceLifetime.Scoped);

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient(typeof(ITransientRepository<>), typeof(TransientRepository<>));

        return services;
    }
}
