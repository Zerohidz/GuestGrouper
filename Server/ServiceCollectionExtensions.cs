using GuestGrouper.Server.Managers;

namespace GuestGrouper.Server;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddManagers(this IServiceCollection services)
    {
        services.AddSingleton<ClientManager>();
        return services;
    }
}
