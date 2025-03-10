﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Arconic.Core.Abstractions.Trends;
using Arconic.Core.Models.PlcData.Drive;
using Arconic.Core.Models.Trends;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using SkiaSharp;

namespace Arconic.View.ViewModels;

public partial class TrendUserDto:TrendBaseViewModel, ITrendUserDto
{
    public TrendUserDto()
    {
         ReInitTrend(new TrendSettings());
    }


    public void ReStyle(TrendSettings trendSettings, bool isArchieve = false)
    {
        if (Mode == MeasModes.CentralLine)
        {
            if (Series is not null && Series.Length > 0 && Series[0] is LineSeries<ObservablePoint> series)
            {
                series.Stroke = new SolidColorPaint(GetSkColor(trendSettings.ThickCurveColor)) { StrokeThickness = 4 };
            }
        }
        else
        {
            if (Series is not null && Series.Length>=3 && Series is LineSeries<ObservablePoint>[] seriesArray)
            {
                seriesArray[0].IsVisible = isArchieve || trendSettings.CurrentScanIsVisible;
                seriesArray[1].IsVisible = isArchieve || trendSettings.PreviousScanIsVisible;
                seriesArray[2].IsVisible = isArchieve || trendSettings.AverageScanIsVisible;
                seriesArray[0].Stroke = new SolidColorPaint(GetSkColor(trendSettings.CurrentScanColor)) { StrokeThickness = 4 };
                seriesArray[1].Stroke = new SolidColorPaint(GetSkColor(trendSettings.PreviousScanColor)) { StrokeThickness = 4 };
                seriesArray[2].Stroke = new SolidColorPaint(GetSkColor(trendSettings.AverageScanColor)) { StrokeThickness = 4 };
            }
        }
    }

    public void ReInit(TrendSettings settings,
        MeasModes mode = MeasModes.ForwRevers, float expectedWidth = 0, float expectedThick = 0,
        float leftBorder = 0, float rightBorder = 0, float centralLine = 0, 
        int scanNumber = 0, bool isArchieve = false)
    {
        Mode = mode;
        ExpectedWidth = expectedWidth;
        ExpectedThick = expectedThick;
        LeftBorder = leftBorder;
        RightBorder = rightBorder;
        CentralLine = centralLine;
        ScanNumber = scanNumber;
        lock (Sync)
        {
            if (Series is not null && Series.Length >= 2)
            {
                 Dispatcher.UIThread.Invoke(() =>
                {
                    Series[1].Values =  new List<ObservablePoint>();
                });
            }
            ActualPoints.Clear();
            ReInitTrend(settings, isArchieve);
            
        }
    }

    private ObservableCollection<ObservablePoint> ActualPoints { get; set; } = new ObservableCollection<ObservablePoint>();

    public void Recalculate(float stripDeviation = 0, float maxThick = 0, float minThick = 0, float actualWidth = 0, float klin = 0,
        float chechevitsa = 0)
    {
        StripDeviation = stripDeviation;
        MaxThick = maxThick;
        MinThick = minThick;
        ActualWidth = actualWidth;
        Klin = klin;
        Chechevitsa = chechevitsa;
    }

    public MeasModes Mode { get; private set; }
    public async void SetPreviousScan(List<ThickPoint>? thickPoints)
    {
        if (Series is not null && Series.Length >= 2)
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                Series[1].Values = thickPoints?.Select(p=> new ObservablePoint(p.Position, p.Thick)).ToList()
                                   ?? new List<ObservablePoint>();
                Series[1].IsVisibleAtLegend = true;

            });
        }
    }

    public async void SetAverageScan(List<ThickPoint>? thickPoints)
    {
        if (Series is not null && Series.Length >= 3)
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                Series[2].Values = thickPoints?.Select(p=> new ObservablePoint(p.Position, p.Thick)).ToList()
                                   ?? new List<ObservablePoint>();
                Series[2].IsVisibleAtLegend = true;

            });
        }
    }

    public void SetActualScan(List<ThickPoint>? thickPoints)
    {
        lock (Sync)
        {
            if (Series is not null && Series.Length >= 1)
            {
                ActualPoints = new ObservableCollection<ObservablePoint>(
                    thickPoints?.Select(p => new ObservablePoint(p.Position, p.Thick))
                    ?? new List<ObservablePoint>());
                Series[0].Values = ActualPoints;
            }
        }
    }

    public void SetTimeCurve(List<ThickPoint>? thickPoints)
    {
        lock (Sync)
        {
            if (Series is not null && Series.Length >= 1)
            {
                Series[0].Values = thickPoints?.Select(p=> new ObservablePoint(p.Lendth, p.Thick)).ToList()
                    ?? new List<ObservablePoint>();
            }
            
        }
    }

    public void ClearActualScan()
    {
        lock (Sync)
        {
            ActualPoints.Clear();
        }
    }
    
    public float ExpectedWidth { get; private set; }
    public float ExpectedThick { get; private set; } = 1000;
    public float LeftBorder { get; private set; }
    public float CentralLine { get; private set; }
    public float RightBorder { get; private set; }
    public float ActualWidth { get; private set; }
    public float MinThick { get; private set; }
    public float MaxThick { get; private set; }
    public float StripDeviation { get; private set; }
    public float Klin { get; set; }
    public int ScanNumber { get; private set; }
    public float Chechevitsa { get; private set; }
    public void AddDateTimeThick(ThickPoint? point)
    {
        lock (Sync)
        {
            if (point != null)
            {
                ActualPoints.Add(new ObservablePoint(point.Lendth, point.Thick));
            }
           
        }
    }

    public void AddPointToScan(ThickPoint? point)
    {
        lock (Sync)
        {
            if (point != null)
            {
                ActualPoints.Add(new ObservablePoint(point.Position, point.Thick));
            }
        }
        
    }

    private void ReInitTrend(TrendSettings settings, bool isArchieve = false)
    {
        if (Mode == MeasModes.CentralLine)
        {
            XAxes =
            [
                new Axis()
                {
                    Name = "Длина, м",
                    InLineNamePlacement = false,
                    NamePaint =  new SolidColorPaint(SKColors.White.WithAlpha(204)),
                    AnimationsSpeed = TimeSpan.FromMilliseconds(0),
                    ShowSeparatorLines = true,
                    Position = AxisPosition.Start,
                    Padding = new Padding(16, 5, 16, 5),
                    LabelsPaint = new SolidColorPaint(SKColors.White.WithAlpha(204)),
                    SeparatorsPaint = new SolidColorPaint(SKColors.White.WithAlpha(128)),
                }
            ];
            Series =  new[]
            {
                new LineSeries<ObservablePoint>
                {
                    Name = "Толщина, мкм",
                    Values = ActualPoints,
                    IsVisible = true,
                    IsVisibleAtLegend = true,
                    GeometryStroke = null,
                    GeometrySize = 0,
                    Fill = null,
                    LineSmoothness = 0,
                    ScalesYAt = 0,
                    Stroke = new SolidColorPaint(GetSkColor(settings.ThickCurveColor)) {StrokeThickness = 4}
                },
                new LineSeries<ObservablePoint>
                {
                    IsVisible = true,
                    Values = [
                        new ObservablePoint(0, ExpectedThick)
                    ],
                    IsVisibleAtLegend = false,
                    GeometryStroke = null,
                    GeometrySize = 0,
                    Fill = null,
                    LineSmoothness = 0,
                    ScalesYAt = 0,
                    Stroke = new SolidColorPaint(SKColors.Transparent){StrokeThickness = 0}
                },
            };
        }
        else
        {
            XAxes =
            [
                new Axis()
                {
                    Name = "Положение рамы, мм",
                    InLineNamePlacement = false,
                    NamePaint =  new SolidColorPaint(SKColors.White.WithAlpha(204)),
                    AnimationsSpeed = TimeSpan.FromMilliseconds(0),
                    ShowSeparatorLines = true,
                    Position = AxisPosition.Start,
                    Padding = new Padding(16, 5, 16, 5),
                    LabelsPaint = new SolidColorPaint(SKColors.White.WithAlpha(204)),
                    SeparatorsPaint = new SolidColorPaint(SKColors.White.WithAlpha(128))
                }
            ];
            
            Series =  new[]
            {
                new LineSeries<ObservablePoint>
                {
                    Name = "Актуальный скан",
                    Values = ActualPoints,
                    IsVisible = isArchieve || settings.CurrentScanIsVisible,
                    IsVisibleAtLegend = true,
                    GeometryStroke = null,
                    GeometrySize = 0,
                    Fill = null,
                    LineSmoothness = 0,
                    ScalesYAt = 0,
                    Stroke = new SolidColorPaint(GetSkColor(settings.CurrentScanColor)){StrokeThickness = 4}
                },
                new LineSeries<ObservablePoint>
                {
                    Name = "Предыдущий скан",
                    IsVisibleAtLegend = false,
                    IsVisible = isArchieve || settings.PreviousScanIsVisible,
                    GeometryStroke = null,
                    GeometrySize = 0,
                    Fill = null,
                    LineSmoothness = 0,
                    ScalesYAt = 0,
                    Stroke = new SolidColorPaint(GetSkColor(settings.PreviousScanColor)){StrokeThickness = 4}
                },
                new LineSeries<ObservablePoint>
                {
                    Name = "Средний скан",
                    IsVisibleAtLegend = false,
                    IsVisible = isArchieve || settings.AverageScanIsVisible,
                    GeometryStroke = null,
                    GeometrySize = 0,
                    Fill = null,
                    LineSmoothness = 0,
                    ScalesYAt = 0,
                    Stroke = new SolidColorPaint(GetSkColor(settings.AverageScanColor)){StrokeThickness = 4}
                },
                new LineSeries<ObservablePoint>
                {
                    IsVisible = true,
                    Values = [
                        new ObservablePoint(CentralLine-ExpectedWidth/2, ExpectedThick),
                        new ObservablePoint(CentralLine+ExpectedWidth/2, ExpectedThick),
                    ],
                    IsVisibleAtLegend = true,
                    GeometryStroke = null,
                    GeometrySize = 0,
                    Fill = new SolidColorPaint(SKColors.Lime.WithAlpha(64)),
                    LineSmoothness = 0,
                    Stroke = new SolidColorPaint(SKColors.Transparent){StrokeThickness = 1}
                },
            };
        }
        SetSections();
        ZoomOut();
    }

    [ObservableProperty]
    private Axis[] _yAxes = [
        new Axis()
        {
            Name = "Толщина, мкм",
            InLineNamePlacement = true,
            NamePaint =  new SolidColorPaint(SKColors.White.WithAlpha(204)),
            AnimationsSpeed = TimeSpan.FromMilliseconds(0),
            ShowSeparatorLines = true,
            Position = AxisPosition.Start,
            Padding = new Padding(16, 5, 16, 5),
            LabelsPaint = new SolidColorPaint(SKColors.White.WithAlpha(204)),
            SeparatorsPaint = new SolidColorPaint(SKColors.White.WithAlpha(128))
        }
    ];
    [ObservableProperty]
    private Axis[] _xAxes  = new Axis[0];

    [ObservableProperty] 
    private ISeries[]? _series;
    
    [ObservableProperty]
    private RectangularSection[] _sections = [
        new()
        {
            Label = "Задание толщины, мкм",
            LabelPaint = new SolidColorPaint(SKColors.White),
            ScalesYAt = 0,
            Stroke = new SolidColorPaint
            {
                Color = SKColors.Red,
                StrokeThickness = 3,
                PathEffect = new DashEffect([6, 6])
            }
        },
        new()
        {
            Label = "Позиция ЦЛ",
            LabelPaint = new SolidColorPaint(SKColors.White),
            ScalesXAt = 0,
            Stroke = new SolidColorPaint
            {
                Color = SKColors.Aqua,
                StrokeThickness = 3,
                PathEffect = new DashEffect([6, 6])
            }
        }
    ];
    
    [RelayCommand]
    private void  ZoomOut()
    {
        lock (Sync)
        {
            if(XAxes.Length > 0)
            {
                XAxes[0].MaxLimit = null;
                XAxes[0].MinLimit = null;
                YAxes[0].MaxLimit = null;
                YAxes[0].MinLimit = null;
            }
        }
    }


    public SolidColorPaint LegendBackgroundColor { get; } = new SolidColorPaint(GetSkColor("#2C2C2E"));
    public SolidColorPaint LegendTextColor { get; } = new SolidColorPaint(GetSkColor("#C0FFFFFF"));


    private void SetSections()
    {
        if (Mode == MeasModes.CentralLine)
        {
            Sections[0].Xi = null;
            Sections[0].Xj = null;
            Sections[0].Yi = ExpectedThick;
            Sections[0].Yj = ExpectedThick;
            Sections[1].IsVisible = false;
        }
        else
        {
            Sections[0].Xi = CentralLine - ExpectedWidth/2;
            Sections[0].Xj = CentralLine + ExpectedWidth/2;
            Sections[0].Yi = 0;
            Sections[0].Yj = ExpectedThick;
            
            Sections[1].IsVisible = true;
            Sections[1].Xi = CentralLine;
            Sections[1].Xj = CentralLine;
        }
        Sections[0].IsVisible = true;
        Sections[0].ScalesXAt = 0;
    }

}