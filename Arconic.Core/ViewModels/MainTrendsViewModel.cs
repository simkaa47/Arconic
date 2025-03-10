﻿using System.Collections.Concurrent;
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

namespace Arconic.Core.ViewModels;

public partial class MainTrendsViewModel:ObservableObject
{
    private event Action? NeedToDoOnScanCompleted;
    private readonly MainPlcService _plcService;
    private readonly ITrendsService _trendsService;
    public Plc Plc { get; }
    private DateTime _lastPointDateTime;
    [ObservableProperty]
    private ParkingMeasure? _parkingMeasure;
    private Scan? _currentScan;
    public TrendSettings TrendSettings { get; }
    
    

    public ITrendUserDto ActualTrend { get; }
    public MainTrendsViewModel(ITrendUserDto actualTrend,
        MainPlcService plcService,
        PlcViewModel plcViewModel, 
        ITrendsService trendsService)
    {
        ActualTrend = actualTrend;
        _plcService = plcService;
        _trendsService = trendsService;
        Plc = plcViewModel.Plc;
        TrendSettings = trendsService.Settings;
        Init();
    }
    [ObservableProperty]
    private Strip? _actualStrip;
    

    private async  void Init()
    {
        Plc.ControlAndIndication.PlcEventsData.StripEnd.PropertyChanged += OnEndStrip;
        Plc.ControlAndIndication.PlcEventsData.StripStart.PropertyChanged += OnStartStrip;
        Plc.ControlAndIndication.MeasureIndicationAndControl.ScanNumber.PropertyChanged += OnScanNumberChanged;
        Plc.ControlAndIndication.MeasureIndicationAndControl.StripUnderFlag.PropertyChanged += OnStripUnder;
        _plcService.PlcScanCompleted += OnPlcScanCompleted;
        _trendsService.Settings.PropertyChanged += (o, args) =>
        {
            ActualTrend.ReStyle( _trendsService.Settings);
        };
    }

    private void OnStripUnder(object? sender, PropertyChangedEventArgs args)
    {
        if (sender is not null && sender is Parameter<bool> strip && args.PropertyName == "Value")
        {
            if (strip.Value && Plc.ControlAndIndication.DriveIndication.IsParkingPosition.Value)
            {
                ParkingMeasure = new ParkingMeasure
                {
                    StartTime = DateTime.Now,
                    ExpectedThick = Plc.ControlAndIndication.HighLevelData.Coils[1].ExpectedThick.Value
                };
            }
            else if(ParkingMeasure is not null)
            {
                ParkingMeasure.EndTime = DateTime.Now;
            }
        }
    }
    private void AddStripToDbAsync()
    {
        Task.Run(async () =>
        {
            if (!await _trendsService.StripExist(ActualStrip))
            {
                await _trendsService.AddStripAsync(ActualStrip);
            }
            else
                await _trendsService.SaveStripAsync(ActualStrip);
        });
        
    }

    private  void OnEndStrip(object? sender, PropertyChangedEventArgs args)
    {
        if (sender is not null && sender is Parameter<bool> endParameter && args.PropertyName == "Value")
        {
            if(args.PropertyName == nameof(endParameter.Value) && endParameter.Value && ActualStrip != null)
            {
                ActualTrend.ClearActualScan();
            }
        }
    }
    
    private void OnStartStrip(object? sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName != "Value") return;
        if (!Plc.ControlAndIndication.PlcEventsData.StripStart.Value) return;
        NeedToDoOnScanCompleted += ReInitActualTrend;
    }

    private  void ReInitActualTrend()
    {
        NeedToDoOnScanCompleted -= ReInitActualTrend;
        ActualStrip = new Strip()
        {
            MeasMode = (MeasModes)Plc.Settings.DriveSettings.MeasMode.Value,
            StripNumber = Plc.ControlAndIndication.HighLevelData.Coils[1].StripId.Value ?? "",
            SteelLabel = Plc.ControlAndIndication.HighLevelData.Coils[1].SteelLabel.Value ?? "",
            StartTime = DateTime.Now,
            CentralLinePosition = Plc.Settings.DriveSettings.CentralPosition.Value,
            ExpectedWidth = Plc.ControlAndIndication.HighLevelData.Coils[1].ExpectedWidth.Value,
            ExpectedThick = Plc.ControlAndIndication.HighLevelData.Coils[1].ExpectedThick.Value,
        };
        _currentScan = new Scan();
        ActualTrend.ReInit(_trendsService.Settings,
            mode:ActualStrip.MeasMode, 
            expectedThick:ActualStrip.ExpectedThick, 
            expectedWidth:ActualStrip.ExpectedWidth, 
            centralLine:ActualStrip.CentralLinePosition, 
            leftBorder: ActualStrip.CentralLinePosition - ActualStrip.ExpectedWidth / 2,
            rightBorder:ActualStrip.CentralLinePosition + ActualStrip.ExpectedWidth / 2);
            AddStripToDbAsync();
    }


    private async void OnScanNumberChanged(object? sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Value" 
            && Plc.Settings.DriveSettings.MeasMode.Value != (short)MeasModes.CentralLine 
            && ActualStrip is not null)
        {
            var scanNum = Plc.ControlAndIndication.MeasureIndicationAndControl.ScanNumber.Value;
            if (_currentScan is { ThickPoints.Count: > 5 } && scanNum>1)
            {
                var plcLastScan = Plc.ControlAndIndication.MeasureIndicationAndControl.PreviousScan;
                var lastScan = _currentScan;
                ActualStrip.Scans.Add(lastScan);
                _currentScan = new Scan();
                lastScan.ScanNumber = plcLastScan.ScanNumber.Value;
                var points = plcLastScan.Points
                    .Take(plcLastScan.PointsNumber.Value)
                    .Select(p => new ThickPoint()
                    {
                        Position = p.Position.Value,
                        Lendth = p.Length.Value,
                        Thick = p.Thick.Value
                    }).ToList();
                lastScan.SetThickPoints(points);
                lastScan.Klin = Plc.ControlAndIndication.MeasureIndicationAndControl.KlinRelative.Value;
                lastScan.Width = Plc.ControlAndIndication.MeasureIndicationAndControl.Width.Value;
                lastScan.Chechewitsa = Plc.ControlAndIndication.MeasureIndicationAndControl.ChechevitsaRelative.Value;
                var lastIndex = ActualStrip.Scans.Count - 1;
                ActualTrend.SetPreviousScan(ActualStrip.Scans[lastIndex].ThickPoints.ToList());
                ActualTrend.ClearActualScan();
                await _trendsService.AddScanToStrip(lastScan, ActualStrip.Id);
                var average = await _trendsService.GetAverageScan(ActualStrip);
                ActualTrend.SetAverageScan(average);
            }
        }
    }
    
    private void PutIntoParkingMeasure()
    {
        if (Plc.ControlAndIndication.MeasureIndicationAndControl.StripUnderFlag.Value &&
            Plc.ControlAndIndication.DriveIndication.IsParkingPosition.Value &&
            ParkingMeasure is not null)
        {
            if (ParkingMeasure.Points.Count == 0 || DateTime.Now > ParkingMeasure.Points[^1].DateTime.AddSeconds(1))
            {
                ParkingMeasure.Points.Add(new TimePoint()
                {
                    DateTime = DateTime.Now,
                    Thick = Plc.ControlAndIndication.MeasureIndicationAndControl.Thick.Value
                });
            }
        }
    }

    private async void OnPlcScanCompleted()
    {
        PutIntoParkingMeasure();
        NeedToDoOnScanCompleted?.Invoke();
        if (Plc.ControlAndIndication.PlcEventsData.StripStart.Value && 
            Plc.ControlAndIndication.MeasureIndicationAndControl.StripUnderFlag.Value)
        {
            var currDt = DateTime.Now;
            if (ActualStrip is not null 
                && Plc.ControlAndIndication.MeasureIndicationAndControl.StripUnderFlag.Value
                && Plc.ControlAndIndication.MeasureIndicationAndControl.Thick.Value>0)
            {
                var delay = ActualStrip.MeasMode == MeasModes.CentralLine ? 50 : 333;
                if (currDt < _lastPointDateTime.AddMilliseconds(delay))
                {
                    return;
                }
                _lastPointDateTime = currDt;
                var point = new ThickPoint()
                {
                    Thick = Plc.ControlAndIndication.MeasureIndicationAndControl.Thick.Value,
                    DateTime = DateTime.Now,
                    Speed = Plc.ControlAndIndication.MeasureIndicationAndControl.Speed.Value,
                    Lendth = Plc.ControlAndIndication.MeasureIndicationAndControl.Length.Value,
                    Position = Plc.ControlAndIndication.DriveIndication.CurrentPosition.Value
                };
                if (ActualStrip.MeasMode == MeasModes.CentralLine)
                {
                    ActualTrend.AddDateTimeThick(point);
                    if (ActualStrip.Id > 0)
                        await _trendsService.AddPointToStrip(point, ActualStrip.Id);
                    else
                        AddStripToDbAsync();
                }
                else
                {
                    if (_currentScan != null)
                    {
                        var plcPoints = Plc.ControlAndIndication.MeasureIndicationAndControl.ActualScan.Points;
                        var plcPointNumber = Plc.ControlAndIndication.MeasureIndicationAndControl.ActualScan
                            .PointsNumber.Value-6;
                        if (plcPointNumber < 0)
                        {
                            return;
                        }
                        var points = plcPoints.Take(plcPointNumber)
                            .Select(p=> new ThickPoint()
                            {
                                Position = p.Position.Value,
                                Thick = p.Thick.Value
                            }).ToList();
                        _currentScan.SetThickPoints(points);
                        ActualTrend.SetActualScan(_currentScan.ThickPoints.ToList());
                    }
                }
                
            }
        }
    }
    [RelayCommand]
    private async Task SaveSettings()
    {
        await _trendsService.SaveTrendSettings();
    }
}