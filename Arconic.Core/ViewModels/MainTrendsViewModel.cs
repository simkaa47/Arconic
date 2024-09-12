using System.ComponentModel;
using Arconic.Core.Abstractions.DataAccess;
using Arconic.Core.Abstractions.Trends;
using Arconic.Core.Models.Parameters;
using Arconic.Core.Models.PlcData;
using Arconic.Core.Models.PlcData.Drive;
using Arconic.Core.Models.Trends;
using Arconic.Core.Services.Plc;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;

namespace Arconic.Core.ViewModels;

public partial class MainTrendsViewModel:ObservableObject
{
    private readonly ILogger<MainTrendsViewModel> _logger;
    private readonly IRepository<Strip> _stripRepository;
    private readonly MainPlcService _plcService;
    private readonly ITrendsService _trendsService;
    private readonly Plc _plc;
    private DateTime _lastPointDateTime;

    public TrendUserDto ActualTrend { get; } = new TrendUserDto();
    public MainTrendsViewModel(ILogger<MainTrendsViewModel> logger, 
        IRepository<Strip> stripRepository, 
        MainPlcService plcService,
        PlcViewModel plcViewModel, 
        ITrendsService trendsService)
    {
        _logger = logger;
        _stripRepository = stripRepository;
        _plcService = plcService;
        _trendsService = trendsService;
        _plc = plcViewModel.Plc;
        InitAsync();
    }
    [ObservableProperty]
    private Strip? _actualStrip;
    [ObservableProperty]
    private IEnumerable<Strip>? _archieveStrips;
    [ObservableProperty]
    private DateTime _startArchieveTime = DateTime.Today;
    [ObservableProperty]
    private DateTime _endArchieveTime = DateTime.Now;
    [ObservableProperty]
    private IEnumerable<TrendUserDto>? _archieveScans;

    private Strip? _selectedArchieveStrip;
    [ObservableProperty]
    private Strip? _selectedArchieveStripForViewing;
    public Strip? SelectedArchieveStrip
    {
        get => _selectedArchieveStrip;
        set
        {
            if (SetProperty(ref _selectedArchieveStrip, value) && SelectedArchieveStrip is not null)
            {
                UpdateSelectedArchieveStrip(SelectedArchieveStrip.Id);
            }
        }
    }

    private async void UpdateSelectedArchieveStrip(long id)
    {
        SelectedArchieveStripForViewing = await _trendsService.GetExtendedStrip(id);
        ArchieveScans = SelectedArchieveStripForViewing != null ?  _trendsService.GetScansFromStrip(SelectedArchieveStripForViewing) : null;
    }

    private async void InitAsync()
    {
        _plc.ControlAndIndication.PlcEventsData.StripEnd.PropertyChanged += OnEndStrip;
        _plc.ControlAndIndication.PlcEventsData.StripStart.PropertyChanged += OnStartStrip;
        _plc.ControlAndIndication.MeasureIndicationAndControl.ScanNumber.PropertyChanged += OnScanNumberChanged;
        _plcService.PlcScanCompleted += OnPlcScanCompleted;
    }


    private async Task AddStripToDbAsync()
    {
        if (!await _trendsService.StripExist(ActualStrip))
        {
            await _trendsService.AddStripAsync(ActualStrip);
        }
        else
            await _trendsService.SaveStripAsync(ActualStrip);
    }

    private async void OnEndStrip(object? sender, PropertyChangedEventArgs args)
    {
        if (sender is not null && sender is Parameter<bool> endParameter && args.PropertyName == "Value")
        {
            if(args.PropertyName == nameof(endParameter.Value) && endParameter.Value && ActualStrip != null)
            {
                await  AddStripToDbAsync();
            }
        }
    }
    
    private async void OnStartStrip(object? sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName != "Value") return;
        if (!_plc.ControlAndIndication.PlcEventsData.StripStart.Value) return;
        ActualStrip = new Strip()
        {
            MeasMode = (MeasModes)_plc.Settings.DriveSettings.MeasMode.Value,
            StripId = _plc.ControlAndIndication.HighLevelData.Coils[1].StripId.Value ?? "",
            SteelLabel = _plc.ControlAndIndication.HighLevelData.Coils[1].SteelLabel.Value ?? "",
            StartTime = DateTime.Now,
            CentralLinePosition = _plc.Settings.DriveSettings.CentralPosition.Value,
            ExpectedWidth = _plc.ControlAndIndication.HighLevelData.Coils[1].ExpectedWidth.Value,
            ExpectedThick = _plc.ControlAndIndication.HighLevelData.Coils[1].ExpectedThick.Value,
        };
        ActualTrend.Mode = ActualStrip.MeasMode;
        ActualTrend.ExpectedThick = ActualStrip.ExpectedThick;
        ActualTrend.ExpectedWidth = ActualStrip.ExpectedWidth;
        ActualTrend.CentralLine = ActualStrip.CentralLinePosition;
        ActualTrend.LeftBorder = ActualStrip.CentralLinePosition - ActualStrip.ExpectedWidth / 2;
        ActualTrend.RightBorder = ActualStrip.CentralLinePosition + ActualStrip.ExpectedWidth / 2;
        ActualTrend.PreviousScan = null;
        ActualTrend.ActualScan.Clear();
        ActualTrend.Thicks.Clear();
        await AddStripToDbAsync();
    }


    private async void OnScanNumberChanged(object? sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value" 
            && _plc.Settings.DriveSettings.MeasMode.Value != (short)MeasModes.CentralLine 
            && ActualStrip is not null)
        {
            if (_plc.ControlAndIndication.MeasureIndicationAndControl.ScanNumber.Value > 0)
            {
                var lastScan = ActualStrip.Scans.LastOrDefault();
                if (lastScan is { ThickPoints.Count: > 0 })
                {
                    _trendsService.AddEdgesAndRecalculate(lastScan, 
                        _plc.ControlAndIndication.MeasureIndicationAndControl.PreviousScan.StartPosition.Value, 
                        _plc.ControlAndIndication.MeasureIndicationAndControl.PreviousScan.EndPosition.Value,
                        ActualStrip);
                    var lastIndex = ActualStrip.Scans.Count - 1;
                    ActualTrend.PreviousScan = ActualStrip.Scans[lastIndex].ThickPoints;
                    ActualTrend.ActualScan.Clear();
                    ActualStrip.Scans.Add(new Scan());
                    await AddStripToDbAsync();
                }
                
            }
        }
    }

    private async void OnPlcScanCompleted()
    {
        if (_plc.ControlAndIndication.PlcEventsData.StripStart.Value && 
            _plc.ControlAndIndication.MeasureIndicationAndControl.StripUnderFlag.Value)
        {
            var currDt = DateTime.Now;
            if (ActualStrip is not null && currDt> _lastPointDateTime.AddMilliseconds(100))
            {
                _lastPointDateTime = currDt;
                var point = new ThickPoint()
                {
                    Thick = _plc.ControlAndIndication.MeasureIndicationAndControl.Thick.Value,
                    DateTime = DateTime.Now,
                    Speed = _plc.ControlAndIndication.MeasureIndicationAndControl.Speed.Value,
                    Lendth = _plc.ControlAndIndication.MeasureIndicationAndControl.Length.Value,
                    Position = _plc.ControlAndIndication.MeasureIndicationAndControl.CaretPosition.Value
                };
                if (ActualStrip.MeasMode == MeasModes.CentralLine)
                {
                    ActualStrip.ThickPoints.Add(point);
                    ActualTrend.Thicks.Add(point);
                    await AddStripToDbAsync();
                }
                else
                {
                    if (ActualStrip.Scans.Count == 0)
                        ActualStrip.Scans.Add(new Scan());
                    var lastScan = ActualStrip.Scans.LastOrDefault();
                    if (lastScan != null)
                    {
                        lastScan.ThickPoints.Add(point);
                        ActualTrend.ActualScan.Add(point);
                    }
                }
                
                
            }
        }
    }
    [RelayCommand]
    private async Task GetArchieveStrips()
    {
        ArchieveStrips = await _trendsService.GetArchieveStrips(StartArchieveTime, EndArchieveTime);
    }
    
    
}