using Microsoft.Extensions.DependencyInjection;
using MerchStore.Application.Services.Implementations;
using MerchStore.Application.Services.Interfaces;

namespace MerchStore.Application;

// Contains extension methods for registering Application layer services with the dependency injection container.
public static class DependencyInjection
{
    // Adds Application layer services to the DI container
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register application services
        services.AddScoped<ICatalogService, CatalogService>();

        return services;
    }
}