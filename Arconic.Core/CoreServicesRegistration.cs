using Arconic.Core.Options;
using Arconic.Core.Services.Plc;
using Arconic.Core.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Arconic.Core;

public static class CoreServicesRegistration
{
    public static void AddCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainPlcService>();
        services.AddSingleton<PlcViewModel>();
        services.Configure<PlcConnectOption>(configuration.GetSection(PlcConnectOption.SectionName));
    }
}