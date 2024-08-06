using Arconic.View.ViewModels;
using Avalonia;
using Avalonia.Controls;

namespace Arconic.View.UserControls.Settings.SourceUnit;

public partial class SourceChartsControl : UserControl
{
    public SourceChartsControl()
    {
        InitializeComponent();
        var app = Application.Current as App;
        this.DataContext = app?.GetService<SourceTrendViewModel>();
    }
}