using System;
using Arconic.Core;
using Arconic.Core.Infrastructure.DataContext.Data;
using Arconic.Core.Services.Events;
using Arconic.Core.Services.Logging;
using Arconic.Core.ViewModels;
using Arconic.View.ViewModels;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Arconic.View;

public partial class App : Application
{
    private IHost? _host;

    private ServiceProvider? _serviceProvider;
    
    public T? GetService<T>() where T : notnull
    {
        T servise = _host!.Services.GetRequiredService<T>();
        return servise;
    }
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        
        _host = Host.CreateDefaultBuilder().ConfigureServices((conf, services) =>
        {
            services.AddCoreServices(conf.Configuration);
            services.AddSingleton<EventsViewModel>();
            services.AddSingleton<SourceTrendViewModel>();
            services.AddSingleton<DetectorsTrendViewModel>();
            services.AddLogging((logging) =>
            {
                logging.AddDatabaseLogging(this.GetService<EventMainService>);
            });
        }).Build();

        using var scope = _host.Services.CreateScope();
        var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
        try
        {
            var context = scope.ServiceProvider.GetRequiredService<ArconicDbContext>();
            context.Database.Migrate();
            ArconicDbContextSeed.Seed(context, loggerFactory);

        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error occured diring migration");                    
        }
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
            desktop.MainWindow.DataContext = GetService<MainViewModel>();
        }
        base.OnFrameworkInitializationCompleted();

        
    }
}