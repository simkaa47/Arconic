using Arconic.View.Dialogs;
using Arconic.View.Dialogs.Access;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Arconic.View.UserControls.HighBar;

public partial class AccessControl : UserControl
{
    public AccessControl()
    {
        InitializeComponent();
    }

    private async void ButtonLogin_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var owner = desktop.MainWindow;
            if (owner is null) return;
            var loginWindow = new AccessDialogWindow();
            await loginWindow.ShowDialog(owner);
        }
    }
}