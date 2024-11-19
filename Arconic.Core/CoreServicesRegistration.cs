using Arconic.Core.Abstractions.DataAccess;
using Arconic.Core.Abstractions.FileAccess;
using Arconic.Core.Abstractions.Security;
using Arconic.Core.Abstractions.Trends;
using Arconic.Core.Infrastructure.DataContext.Data;
using Arconic.Core.Infrastructure.DataContext.Repositories;
using Arconic.Core.Infrastructure.Security;
using Arconic.Core.Options;
using Arconic.Core.Services.Access;
using Arconic.Core.Services.Database;
using Arconic.Core.Services.Events;
using Arconic.Core.Services.Plc;
using Arconic.Core.Services.Trends;
using Arconic.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Arconic.Core;

public static class CoreServicesRegistration
{
    public static void AddCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ArconicDbContext>((options) =>
        {
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.UseSqlite(configuration.GetConnectionString("Sqlite"));
        });
        
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<EventMainService>();
        services.AddSingleton<AccessService>();
        services.AddSingleton<CoolingViewModel>();
        services.AddHostedService<ClearDatabaseService>();
        services.AddSingleton<SafetyViewModel>();
        services.AddSingleton<MainTrendsViewModel>();
        services.AddSingleton<SingleMeasuresViewModel>();
        services.AddSingleton<SteelMagazineViewModel>();
        
        services.AddSingleton<AccessViewModel>();
        services.AddTransient<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<MainPlcService>();
        services.AddSingleton<PlcViewModel>();
        services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddTransient<ITrendsService, TrendsService>();
        services.Configure<PlcConnectOption>(configuration.GetSection(PlcConnectOption.SectionName));
        services.Configure<DbClearOption>(configuration.GetSection(DbClearOption.SectionName));
        
    }
}