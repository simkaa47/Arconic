using Arconic.Core.Abstractions.Common;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Logging;

namespace Arconic.View.UserControls.HighBar;

public partial class HighBarControl : UserControl
{
    public HighBarControl()
    {
        InitializeComponent();
    }

    private async void CloseWindowButtonOnClick(object? sender, RoutedEventArgs e)
    {
        if (Application.Current is App app && Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var dialog = app.GetService<IQuestionDialog>();
            if (dialog != null)
            {
                if (await dialog.AskAsync("Закрытие приложения", "Закрыть приложение?"))
                {
                    var logger = app.GetService<ILogger<HighBarControl>>();
                    if (logger != null)
                    {
                        logger.LogInformation("Нажата кнопка закрытия приложения");
                        desktop.MainWindow?.Close();
                    }
                }
            }
        }
    }

    private void MinimizeWindowButtonOnClick(object? sender, RoutedEventArgs e)
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime { MainWindow: not null } desktop)
        {
            desktop.MainWindow.WindowState = WindowState.Minimized;
        }
    }
    
    private void NormalWindowButtonOnClick(object? sender, RoutedEventArgs e)
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop && desktop.MainWindow is not null)
        {
            desktop.MainWindow.WindowState = WindowState.Normal;
        }
    }
    
    private void MaximizeWindowButtonOnClick(object? sender, RoutedEventArgs e)
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop && desktop.MainWindow is not null)
        {
            desktop.MainWindow.WindowState = WindowState.FullScreen;
        }
    }
}