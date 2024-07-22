using Arconic.Core;
using Arconic.Core.ViewModels;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Arconic.View;

public partial class App : Application
{
    private readonly IHost _host;
    
    public T GetService<T>() where T : notnull
    {
        T servise = _host.Services.GetRequiredService<T>();
        return servise;
    }
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public App()
    {
        _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
        {
            services.AddCoreServices();
        }).Build();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
            desktop.MainWindow.DataContext = GetService<MainViewModel>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}