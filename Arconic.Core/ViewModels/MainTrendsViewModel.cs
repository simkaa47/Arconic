using System.ComponentModel;
using System.Globalization;
using Arconic.Core.Abstractions.Common;
using Arconic.Core.Abstractions.DataAccess;
using Arconic.Core.Abstractions.FileAccess;
using Arconic.Core.Abstractions.Trends;
using Arconic.Core.Models.Parameters;
using Arconic.Core.Models.PlcData;
using Arconic.Core.Models.PlcData.Drive;
using Arconic.Core.Models.Trends;
using Arconic.Core.Services.Plc;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Spire.Xls;

namespace Arconic.Core.ViewModels;

public partial class MainTrendsViewModel:ObservableObject
{
    private event Action? NeedToDoOnScanCompleted;
    
    private readonly MainPlcService _plcService;
    private readonly IQuestionDialog _dialog;
    private readonly IFileDialog _fileDialog;
    private readonly ITrendsService _trendsService;
    public Plc Plc { get; }
    private DateTime _lastPointDateTime;
    [ObservableProperty]
    private ParkingMeasure? _parkingMeasure;

    public ITrendUserDto ActualTrend { get; }
    public MainTrendsViewModel(ITrendUserDto actualTrend,
        MainPlcService plcService,
        IQuestionDialog dialog,
        IFileDialog fileDialog,
        PlcViewModel plcViewModel, 
        ITrendsService trendsService)
    {
        ActualTrend = actualTrend;
        _plcService = plcService;
        _dialog = dialog;
        _fileDialog = fileDialog;
        _trendsService = trendsService;
        Plc = plcViewModel.Plc;
        Init();
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
    private List<ITrendUserDto>? _archieveScans;
    [ObservableProperty]
    private ITrendUserDto? _currentArchieveScan;
    private int _archieveScanIndex = 0;
    [ObservableProperty]
    private bool _showPrevScanEnabled;
    [ObservableProperty]
    private bool _showNextScanEnabled;
    [ObservableProperty]
    private bool _isArchieveScanLoading;
    
    

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
                Task.Run(() =>
                {
                    UpdateSelectedArchieveStrip(SelectedArchieveStrip.Id);
                });

            }
        }
    }

    private async void UpdateSelectedArchieveStrip(long id)
    {
        IsArchieveScanLoading = true;
        SelectedArchieveStripForViewing = await _trendsService.GetExtendedStrip(id);
        ArchieveScans = SelectedArchieveStripForViewing != null ?  _trendsService.GetScansFromStrip(SelectedArchieveStripForViewing) : null;
        CurrentArchieveScan = ArchieveScans?.FirstOrDefault();
        _archieveScanIndex = 0;
        EnableChangeScanNumber();
        IsArchieveScanLoading = false;
    }

    private  void Init()
    {
        //Plc.ControlAndIndication.PlcEventsData.StripEnd.PropertyChanged += OnEndStrip;
        Plc.ControlAndIndication.PlcEventsData.StripStart.PropertyChanged += OnStartStrip;
        Plc.ControlAndIndication.MeasureIndicationAndControl.ScanNumber.PropertyChanged += OnScanNumberChanged;
        Plc.ControlAndIndication.MeasureIndicationAndControl.StripUnderFlag.PropertyChanged += OnStripUnder;
        _plcService.PlcScanCompleted += OnPlcScanCompleted;
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
                AddStripToDbAsync();
            }
        }
    }
    
    private async void OnStartStrip(object? sender, PropertyChangedEventArgs args)
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
        ActualTrend.ReInit(mode:ActualStrip.MeasMode, 
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
            if (scanNum > 0)
            {
                var plcLastScan = Plc.ControlAndIndication.MeasureIndicationAndControl.PreviousScan;
                var lastScan = ActualStrip.Scans.LastOrDefault();
                
                if (lastScan is { ThickPoints.Count: > 5 } && scanNum>1)
                {
                    lastScan.ScanNumber = plcLastScan.ScanNumber.Value;
                    lastScan.ThickPoints = plcLastScan.Points
                        .Take(plcLastScan.PointsNumber.Value)
                        .Select(p => new ThickPoint()
                        {
                            Position = p.Position.Value,
                            Lendth = p.Length.Value,
                            Thick = p.Thick.Value
                        }).ToList();
                    _trendsService.RecalculateScan(lastScan, ActualStrip);
                    lastScan.Klin = plcLastScan.Klin.Value;
                    lastScan.Width = plcLastScan.Width.Value;
                    lastScan.Chechewitsa = plcLastScan.Chehevitsa.Value;
                    var lastIndex = ActualStrip.Scans.Count - 1;
                    ActualTrend.SetPreviousScan(ActualStrip.Scans[lastIndex].ThickPoints);
                    ActualTrend.ClearActualScan();
                    await _trendsService.AddScanToStrip(lastScan, ActualStrip.Id);
                }
                ActualStrip.Scans.Add(new Scan());
                
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
                if (currDt < _lastPointDateTime.AddMilliseconds(delay)) return;
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
                    if (ActualStrip.Scans.Count == 0)
                        ActualStrip.Scans.Add(new Scan());
                    var lastScan = ActualStrip.Scans.LastOrDefault();
                    if (lastScan != null)
                    {
                        var plcPoints = Plc.ControlAndIndication.MeasureIndicationAndControl.ActualScan.Points;
                        var plcPointNumber = Plc.ControlAndIndication.MeasureIndicationAndControl.ActualScan
                            .PointsNumber.Value-6;
                        if (plcPointNumber < 0) return;    
                        lastScan.ThickPoints = plcPoints.Take(plcPointNumber)
                            .Select(p=> new ThickPoint()
                            {
                                Position = p.Position.Value,
                                Thick = p.Thick.Value
                            }).ToList();
                        ActualTrend.SetActualScan(lastScan.ThickPoints);
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
    [RelayCommand]
    private async Task ShowNextArchieveScanAsync()
    {
        if (ShowNextScanEnabled)
        {
            IsArchieveScanLoading = true;
            await Task.Run(() =>
            {
                CurrentArchieveScan = ArchieveScans?[++_archieveScanIndex] ?? null;
            });
        }
        IsArchieveScanLoading = false;
        EnableChangeScanNumber();
    }
    [RelayCommand]
    private async Task ShowPreviousArchieveScanAsync()
    {
        
        if (ShowPrevScanEnabled)
        {
            IsArchieveScanLoading = true;
            await Task.Run(() =>
            {
                CurrentArchieveScan = ArchieveScans?[--_archieveScanIndex] ?? null;
            });
        }
        EnableChangeScanNumber();
        IsArchieveScanLoading = false;
    }

    private void EnableChangeScanNumber()
    {
        if (ArchieveScans is null || ArchieveScans.Count == 0)
        {
            ShowNextScanEnabled = false;
            ShowPrevScanEnabled = false;
        }
        else
        {
            ShowPrevScanEnabled = _archieveScanIndex > 0;
            ShowNextScanEnabled = _archieveScanIndex < ArchieveScans.Count - 1;
        }
    }


    [RelayCommand]
    private async Task  ExportScanAsync(object? o)
    {
        if (o is not null && o is Strip strip)
        {
            try
            {
                var directory = await _fileDialog.GetDirectory();
                if (string.IsNullOrEmpty(directory)) return;
                var fileName = $"{strip.StartTime:dd_MM_yyyy_HH_mm_ss}_{strip.StripNumber}.xlsx";
                await SaveToXlsAsync($"{directory}{fileName}", strip);
                await _dialog.AskAsync("Успех!", $"Сохранение данных полосы в {directory}{fileName} выполнено успешно");

                
            }
            
            catch (Exception e)
            {
                await _dialog.AskAsync("Ошибка при сохранении полосы", e.Message);
            }
            
        }
    }


    private async Task SaveToXlsAsync(string path, Strip strip)
    {
        await Task.Run(() =>
        {
            Workbook workbook = new Workbook();
                if (strip.MeasMode == MeasModes.CentralLine)
                {
                    Worksheet worksheet = workbook.Worksheets[0];
                    worksheet.Range[1, 1].Value = "Дата и время";
                    worksheet.Range[1, 2].Value = "Длина, м";
                    worksheet.Range[1, 3].Value = "Толщина, мкм";

                    for (int i = 0; i < strip.ThickPoints.Count; i++)
                    {
                        worksheet.Range[2+i, 1].DateTimeValue = strip.ThickPoints[i].DateTime;
                        worksheet.Range[2+i, 1].Style.NumberFormat = "dd.MM.yyyy hh:mm:ss.00";
                        worksheet.Range[2+i, 2].Value = strip.ThickPoints[i].Lendth.ToString("f3");
                        worksheet.Range[2+i, 3].Value = strip.ThickPoints[i].Thick.ToString("f3");
                    }
                
                    worksheet.AllocatedRange.AutoFitColumns();
                
                    CellStyle style = workbook.Styles.Add("newStyle");
                    style.Font.IsBold = true;
                    worksheet.Range[1, 1, 1, 3].Style = style;
                }
                else
                {
                    CellStyle style = workbook.Styles.Add("newStyle");
                    style.Font.IsBold = true;

                    if (workbook.Worksheets.Count < strip.Scans.Count)
                    {
                        workbook.CreateEmptySheets(strip.Scans.Count);
                    }
                    
                    for (int i = 0; i < strip.Scans.Count; i++)
                    {
                        Worksheet worksheet = workbook.Worksheets[i];
                        worksheet.Name = $"Скан № {i + 1}";
                        worksheet.Range[1, 1].Value = "Длина, м";
                        worksheet.Range[1, 2].Value = "Положение рамы, мм";
                        worksheet.Range[1, 3].Value = "Толщина, мкм";
                        worksheet.Range[1, 1, 1, 3].Style = style;
                        for (int j = 0; j < strip.Scans[i].ThickPoints.Count; j++)
                        {
                            worksheet.Range[2+j, 1].Value = strip.Scans[i].ThickPoints[j].Lendth.ToString("f3");
                            worksheet.Range[2+j, 2].Value = strip.Scans[i].ThickPoints[j].Position.ToString("f0");
                            worksheet.Range[2+j, 3].Value = strip.Scans[i].ThickPoints[j].Thick.ToString("f3");
                        }
                        worksheet.AllocatedRange.AutoFitColumns();
                    }
                    
                }
                workbook.SaveToFile(path, ExcelVersion.Version2016);
                
        });
    }
}