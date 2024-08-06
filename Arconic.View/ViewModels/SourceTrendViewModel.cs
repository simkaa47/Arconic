using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Arconic.Core.Services.Plc;
using Arconic.Core.ViewModels;
using Avalonia.Media;
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

public partial class SourceTrendViewModel:ObservableObject
{
    private const string VoltageAxisName = "U, кВ";
    private const string CurrentAxisName = "I, мA";
    private readonly MainPlcService _plcService;
    private readonly PlcViewModel _plcViewModel;

    public SourceTrendViewModel(MainPlcService plcService, PlcViewModel plcViewModel)
    {
        _plcService = plcService;
        _plcViewModel = plcViewModel;
        Init();
    }
    private DateTime _lastTimeUpdated;

    public ObservableCollection<DateTimePoint>[] Values { get; } =
        Enumerable.Range(0, 2).Select(i => new ObservableCollection<DateTimePoint>()).ToArray();
    public object Sync { get; } = new object();
    [ObservableProperty]
    private DrawMarginFrame? _drawMarginFrame;

    public ISeries[]? Series { get; private set; } 
    public LiveChartsCore.Measure.Margin Margin { get; set; } = new LiveChartsCore.Measure.Margin(16);
    
    public Axis[] YAxes { get; set; } =
    {
        new Axis()
        {
            Name = VoltageAxisName,
            InLineNamePlacement = true,
            NamePaint =  new SolidColorPaint(SKColors.Aqua),
            AnimationsSpeed = TimeSpan.FromMilliseconds(0),
            ShowSeparatorLines = true,
            Position = AxisPosition.Start,
            Padding = new Padding(16, 5, 16, 5),
            LabelsPaint = new SolidColorPaint(SKColors.Aqua),
            SeparatorsPaint = new SolidColorPaint(SKColors.White.WithAlpha(128))
        },
        new Axis()
        {
            Name = CurrentAxisName,
            InLineNamePlacement = true,
            NamePaint = new SolidColorPaint(SKColors.Red),
            AnimationsSpeed = TimeSpan.FromMilliseconds(0),
            ShowSeparatorLines = true,
            Position = AxisPosition.End,
            Padding = new Padding(32, 5, 16, 5),
            LabelsPaint = new SolidColorPaint(SKColors.Red),
            SeparatorsPaint = new SolidColorPaint(SKColors.White.WithAlpha(128))
        }
    };
    public Axis[] XAxes { get; set; } =
    {
        new DateTimeAxis(TimeSpan.FromSeconds(1), Formatter)
        {
            AnimationsSpeed = TimeSpan.FromMilliseconds(0),
            ShowSeparatorLines = true,
            Padding = new Padding(16, 32, 16, 32),
            LabelsPaint = new SolidColorPaint(SKColors.White.WithAlpha(204)),
            LabelsRotation = 90,                
            Labeler = (d)=>{
                var ticks = (long)d;
                if(ticks>0)
                {
                    var dt = new DateTime(ticks);
                    return dt.ToString("HH:mm:ss");
                }
                return d.ToString();
            },
            SeparatorsPaint = new SolidColorPaint(SKColors.White.WithAlpha(128))
        }
    };

    [ObservableProperty]
    private RectangularSection[]? _sections;
    
    
    private static string Formatter(DateTime date)
    {
        return date.ToString("G");
    }
    
    private SKColor GetSKColor(string colorString)
    {
        var color = Color.Parse(colorString);
        var skColor = new SKColor(color.R, color.G, color.B, color.A);
        return skColor;
    }
    
    [RelayCommand]
    private void EnableRealTime()
    {
        if (XAxes is not null && XAxes.Length > 0)
        {
            XAxes[0].MaxLimit = null;
            XAxes[0].MinLimit = null;
            YAxes[0].MaxLimit = null;
            YAxes[0].MinLimit = null;
            YAxes[1].MaxLimit = null;
            YAxes[1].MinLimit = null;
        }
    }

    private  void Init()
    {
        Series =  new[]
        {
            new LineSeries<DateTimePoint>
            {
                Name = VoltageAxisName,
                Values = Values[0],
                IsVisible = true,
                IsVisibleAtLegend = true,
                GeometryStroke = null,
                GeometrySize = 0,
                Fill = null,
                LineSmoothness = 0,
                ScalesYAt = 0,
                Stroke = new SolidColorPaint(SKColors.Aqua)
            },
            new LineSeries<DateTimePoint>
            {
                Name = CurrentAxisName,
                Values = Values[1],
                IsVisible = true,
                LineSmoothness = 0,
                ScalesYAt = 1, 
                IsVisibleAtLegend = true,
                GeometryStroke = null,
                GeometrySize = 0,
                Fill = null,
                Stroke = new SolidColorPaint(SKColors.Red),
            }
            
        };
        SetStyle();
        _plcService.PlcScanCompleted += AddDataAsync;
    }

    private void SetStyle()
    {
        XAxes[0].LabelsPaint = new SolidColorPaint(SKColors.White.WithAlpha(204));
        XAxes[0].SeparatorsPaint = new SolidColorPaint(SKColors.White.WithAlpha(128));
        YAxes[0].LabelsPaint = new SolidColorPaint(SKColors.Aqua);
        YAxes[0].SeparatorsPaint = new SolidColorPaint(SKColors.White.WithAlpha(128));
        YAxes[1].LabelsPaint = new SolidColorPaint(SKColors.Red);
        YAxes[1].SeparatorsPaint = new SolidColorPaint(SKColors.White.WithAlpha(128));
        
        
        
        DrawMarginFrame  =  new DrawMarginFrame
        {
            Fill = new SolidColorPaint(GetSKColor("#2C2C2E")),
            Stroke = new SolidColorPaint(SKColors.White.WithAlpha(128))
        };
    }
    private Random rnd  = new Random();
    

    private void AddDataAsync()
    {
        var dt = DateTime.Now;
        if (dt >= _lastTimeUpdated.AddMilliseconds(1000))
        {
            lock (Sync)
            {
                SetSections();
                Values[0].Add(new DateTimePoint(dt, _plcViewModel.Plc.ControlAndIndication.BsIndication.Voltage.Value));
                Values[1].Add(new DateTimePoint(dt, _plcViewModel.Plc.ControlAndIndication.BsIndication.Current.Value));
                foreach (var list in Values)
                {
                    if( list[0].DateTime.AddHours(1)<=dt)
                        list.RemoveAt(0);
                }
                _lastTimeUpdated = dt;
            }
        }
    }

    private void SetSections()
    {
        Sections = new []{
            new RectangularSection
            {
                Yi = _plcViewModel.Plc.Settings.Source.VoltageSv.Value,
                Yj = _plcViewModel.Plc.Settings.Source.VoltageSv.Value,
                Stroke = new SolidColorPaint
                {
                    Color = SKColors.Aqua,
                    StrokeThickness = 3,
                    PathEffect = new DashEffect(new float[] { 6, 6 })
                }
            },
            new RectangularSection
            {
                Yi = _plcViewModel.Plc.Settings.Source.CurrentSv.Value,
                Yj = _plcViewModel.Plc.Settings.Source.CurrentSv.Value,
                Stroke = new SolidColorPaint
                {
                    Color = SKColors.Red,
                    StrokeThickness = 3,
                    PathEffect = new DashEffect(new float[] { 6, 6 })
                }
            }
        };
    }
    
    
}