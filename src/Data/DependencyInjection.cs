using Data.Resilience;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IRepositoryResilience, RepositoryResilience>();
        
        return serviceCollection;
    }
}