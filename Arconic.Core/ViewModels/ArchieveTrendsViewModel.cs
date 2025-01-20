using Arconic.Core.Abstractions.Common;
using Arconic.Core.Abstractions.FileAccess;
using Arconic.Core.Abstractions.Trends;
using Arconic.Core.Models.PlcData.Drive;
using Arconic.Core.Models.Trends;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Spire.Xls;

namespace Arconic.Core.ViewModels;

public partial class ArchieveTrendsViewModel(ITrendsService trendsService, 
    IQuestionDialog dialog,
    IFileDialog fileDialog) : ObservableObject
{
    private readonly ITrendsService _trendsService = trendsService;
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
                var directory = await fileDialog.GetDirectory();
                if (string.IsNullOrEmpty(directory)) return;
                var fileName = $"{strip.StartTime:dd_MM_yyyy_HH_mm_ss}_{strip.StripNumber}.xlsx";
                await SaveToXlsAsync($"{directory}{fileName}", strip);
                await dialog.AskAsync("Успех!", $"Сохранение данных полосы в {directory}{fileName} выполнено успешно");
                
            }
            
            catch (Exception e)
            {
                await dialog.AskAsync("Ошибка при сохранении полосы", e.Message);
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