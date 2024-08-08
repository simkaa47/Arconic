using Arconic.View.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Arconic.View.UserControls.Settings.Detectors;

public partial class DetectorsTrendControl : UserControl
{
    public DetectorsTrendControl()
    {
        InitializeComponent();
        var app = Application.Current as App;
        this.DataContext = app?.GetService<DetectorsTrendViewModel>();
    }
}