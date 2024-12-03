using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using LiveChartsCore.Drawing;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Avalonia;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace Arconic.View.UserControls.MainTab;

public partial class LiveChartsTrendTemplate : UserControl
{
    public LiveChartsTrendTemplate()
    {
        InitializeComponent();
    }
}