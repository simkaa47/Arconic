using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Arconic.View.UserControls.MainTab.Trends;

public partial class CurrentTrendsControl : UserControl
{
    public CurrentTrendsControl()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Popup is not null)
        {
            Popup.IsOpen = false;
        }
    }
}