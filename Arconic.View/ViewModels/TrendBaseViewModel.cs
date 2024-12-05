using System;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace Arconic.View.ViewModels;

public partial class TrendBaseViewModel:ObservableValidator
{
    protected TrendBaseViewModel()
    {
        DrawMarginFrame = new DrawMarginFrame
        {
            Fill = new SolidColorPaint(GetSkColor("#2C2C2E")),
            Stroke = new SolidColorPaint(SKColors.White.WithAlpha(128))
        };
    }
    
    public LiveChartsCore.Measure.Margin Margin { get; set; } = new LiveChartsCore.Measure.Margin(16,26,16,16);
    public object Sync { get; } = new object();
    [ObservableProperty] 
    private DrawMarginFrame? _drawMarginFrame;

    protected static SKColor GetSkColor(string colorString)
    {
        var color = Color.Parse(colorString);
        var skColor = new SKColor(color.R, color.G, color.B, color.A);
        return skColor;
    }
    protected static string Formatter(DateTime date)
    {
        return date.ToString("G");
    }
}