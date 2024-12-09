using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Arconic.Core.Models.Event;
using Arconic.Core.Models.PlcData;
using Arconic.Core.Services.Events;
using Arconic.Core.Services.Plc;
using Arconic.Core.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.Extensions.Logging;
using SkiaSharp;

namespace Arconic.View.ViewModels;

public partial class DetectorsTrendViewModel : TrendBaseViewModel
{
    public DetectorsTrendViewModel(PlcViewModel plcViewModel, 
        MainPlcService mainPlcService, 
        EventMainService eventMainService,
        ILogger<DetectorsTrendViewModel> logger)
    {
        _plcViewModel = plcViewModel;
        _plc = plcViewModel.Plc;
        _mainPlcService = mainPlcService;
        _eventMainService = eventMainService;
        _logger = logger;
        _mainPlcService.PlcScanCompleted += OnPlcScanCompleted;
        Series[0].Values = Enumerable.Range(0, 31).Select(i => 0f).ToList();
        SetStyle();
    }
    
    private readonly PlcViewModel _plcViewModel;
    private readonly MainPlcService _mainPlcService;
    private readonly EventMainService _eventMainService;
    private readonly ILogger<DetectorsTrendViewModel> _logger;
    private readonly Plc _plc;
    

    [ObservableProperty]
    private DetectorsTrendMode _mode;
    [ObservableProperty]
    [Range(0,31)]
    private int _minWriteIndex = 0;
    
    [ObservableProperty]
    [Range(0,31)]
    private int _maxWriteIndex = 31;
    
    [ObservableProperty]
    [Range(0,65535)]
    private ushort _writeValue;
    
    public ISeries[] Series { get; set; } =
    {
        new ColumnSeries<float>
        {
            // Defines the distance between every bars in the series
            Padding = 0,
            DataLabelsPaint = new SolidColorPaint(SKColors.White.WithAlpha(128)),
            DataLabelsRotation = 270,
            DataLabelsFormatter = (point) => point.Coordinate.PrimaryValue.ToString("f1"),
            AnimationsSpeed = TimeSpan.Zero,
            // Defines the max width a bar can have
            MaxBarWidth = double.PositiveInfinity
        }
    };

    private void SetStyle()
    {
        
    }
    
    public Axis[] YAxes { get; set; } =
    {
        new Axis()
        {
            Name = "Значения АЦП детекторов, каунты",
            InLineNamePlacement = false, MinLimit = 0,
            NamePaint =  new SolidColorPaint(SKColors.White.WithAlpha(128)),
            ShowSeparatorLines = true,
            LabelsPaint = new SolidColorPaint(SKColors.White.WithAlpha(128)),
            SeparatorsPaint = new SolidColorPaint(SKColors.White.WithAlpha(128))
        }
    };
    
    public Axis[] XAxes { get; set; } =
    {
        new Axis()
        {
            SubseparatorsCount = 0,
            ShowSeparatorLines = false,
            NamePaint =  new SolidColorPaint(SKColors.White.WithAlpha(128)),
            LabelsPaint = new SolidColorPaint(SKColors.White.WithAlpha(128))
        }
    };

    private void OnPlcScanCompleted()
    {
        lock (Sync)
        {
            switch (Mode)
            {
                case DetectorsTrendMode.Detectors:
                    Series[0].Values = _plc.ControlAndIndication.DetectorsIndication.Values.Select(v => v.Value).ToList();
                    break;
                case DetectorsTrendMode.StandOpen0:
                    Series[0].Values = _plc.Settings.DetectorsSettings.Diap0.Open.Select(v => v.Value).ToList();
                    break;
                case DetectorsTrendMode.StandClose0:
                    Series[0].Values = _plc.Settings.DetectorsSettings.Diap0.Close.Select(v => v.Value).ToList();
                    break;
                case DetectorsTrendMode.Emulations:
                    Series[0].Values = _plc.Settings.DetectorsSettings.EmulationValues.Select(v => (float)v.Value);
                    break;
            }
        }
    }

    
    [RelayCommand]
    private async Task WriteValues()
    {
        if (Mode == DetectorsTrendMode.Emulations)
        {
            if (MinWriteIndex >= 0 && MinWriteIndex <= 31
                                   && MaxWriteIndex >= 0 && MaxWriteIndex <= 31)
            {
                await _eventMainService.OnEventOccure(new EventHistoryItem()
                {
                    Message =
                        $"Нажата кнопка записи значений эмуляций детекторов с записью с {MinWriteIndex} по {MaxWriteIndex} номера детекторов",
                    EventType = EventType.HmiEvent

                });
                for (int i = MinWriteIndex; i <= MaxWriteIndex; i++)
                {
                    lock (Sync)
                    {
                        _plc.Settings.DetectorsSettings.EmulationValues[i].WriteValue = WriteValue;
                        _mainPlcService.WriteParameter(_plc.Settings.DetectorsSettings.EmulationValues[i]);
                    }
                }
            }
        }
    }

}