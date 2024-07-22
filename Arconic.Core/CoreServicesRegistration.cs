using Arconic.Core.Services.Plc;
using Arconic.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Arconic.Core;

public static class CoreServicesRegistration
{
    public static void AddCoreServices(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainPlcService>();
    }
}