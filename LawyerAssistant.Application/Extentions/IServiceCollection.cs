using LawyerAssistant.Application.Contracts.Common;
using Microsoft.Extensions.DependencyInjection;

namespace LawyerAssistant.Application.Extentions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection RegisterAllDependencyInjection<TEntity>(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var types = assemblies.SelectMany(c => c.GetExportedTypes()).Where(t => t.IsClass && !t.IsAbstract && t.IsPublic && typeof(TEntity).IsAssignableFrom(t));
        foreach (var type in types)
        {
            var interfaceType = assemblies.SelectMany(c => c.GetExportedTypes()).First(t => t.IsInterface && t.Name == $"I{type.Name}");

            if (typeof(TEntity) == typeof(IScoped))
                services.AddScoped(interfaceType, type);

            if (typeof(TEntity) == typeof(ISingleton))
                services.AddSingleton(interfaceType, type);

            if (typeof(TEntity) == typeof(ITransient))
                services.AddTransient(interfaceType, type);
        }


        return services;
    }
}
