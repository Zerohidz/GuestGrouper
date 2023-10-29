using BlazorWebAssemblyTest.Server.Managers;

namespace BlazorWebAssemblyTest.Server;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddManagers(this IServiceCollection services)
    {
        services.AddSingleton<PersonManager>();
        return services;
    }
}
