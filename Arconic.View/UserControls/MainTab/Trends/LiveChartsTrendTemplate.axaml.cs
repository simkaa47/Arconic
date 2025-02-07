using Avalonia;
using Avalonia.Controls;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Avalonia;
using LiveChartsCore.SkiaSharpView.Drawing;

namespace Arconic.View.UserControls.MainTab.Trends;

public partial class LiveChartsTrendTemplate : UserControl
{
    private CartesianChart lvc;
    Grid grid;
    public LiveChartsTrendTemplate()
    {
        InitializeComponent();
        lvc = this.GetControl<CartesianChart>("Cc");
        grid = this.GetControl<Grid>("Grid");
        lvc.SizeChanged += (sender, args) =>
        {
            OnSomethingChanged();
        };
        lvc.PropertyChanged += (sender, args) =>
        {
            OnSomethingChanged();
        };
        this.Loaded += (sender, args) =>
        {
            OnSomethingChanged();
        };
    }

    private void OnSomethingChanged()
    {
        double chartWidth = 0;
        double chartOffsetX = 0;
        if (lvc.CoreChart is CartesianChart<SkiaSharpDrawingContext> chart)
        {
            chartWidth = chart.DrawMarginSize.Width;
            chartOffsetX = chart.DrawMarginLocation.X-20;
        }
        if(chartOffsetX>0)
            grid.Margin = new Thickness(chartOffsetX, 0, 0, 0);
        if(chartWidth>0)
            grid.Width = chartWidth;
    }
}