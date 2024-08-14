using Arconic.View.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Arconic.View.UserControls.Errors;

public partial class ErrorsMainControl : UserControl
{
    public ErrorsMainControl()
    {
        InitializeComponent();
        var app = Application.Current as App;
        this.DataContext = app?.GetService<EventsViewModel>();
    }
}