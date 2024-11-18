using Arconic.Core.Models.Parameters;
using Arconic.Core.Models.PlcData;
using Arconic.Core.Models.PlcData.SingleMeasures;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;

namespace Arconic.Core.ViewModels;

public partial class SingleMeasuresViewModel:ObservableObject
{
    public PlcViewModel PlcViewModel { get; }
    private readonly ILogger<SingleMeasuresViewModel> _logger;

    public SingleMeasuresViewModel(ILogger<SingleMeasuresViewModel> logger, PlcViewModel plcViewModel)
    {
        PlcViewModel = plcViewModel;
        _logger = logger;
        Plc = plcViewModel.Plc;
        SingleMeasuresList = GetSingleMeasuresListFromDiapasone(0);
        Init();
    }
    [ObservableProperty]
    private int _selectedDiapasone;
    [ObservableProperty]
    private List<SingleMeasCell> _singleMeasuresList;

    public Plc Plc { get; }

    private  void Init()
    {
        foreach (var item in Plc.Settings.SteelSettings.SteelItems.Select(s=>s.Name))
        {
            item.PropertyChanged += (s, args) =>
            {
                if (args.PropertyName == "Value")
                {
                    GetSteelLabels();
                }
            };
        }
        foreach (var isChecked in Plc.Settings.SingleMeasures.
                     SelectMany(s=>s.Measures)
                     .Select(mc=>mc.IsChecked))
        {
            isChecked.PropertyChanged += (o, args) =>
            {
                if (args.PropertyName == "Value")
                {
                    SingleMeasuresList = GetSingleMeasuresListFromDiapasone(SelectedDiapasone);
                }
            };

        }
    }

    [RelayCommand]
    private void OnChangeDiapasone()
    {
        SingleMeasuresList = GetSingleMeasuresListFromDiapasone(SelectedDiapasone);
    }
    
    [ObservableProperty]
    private IEnumerable<string?>? _steelLabels;


    private void GetSteelLabels()
    {
        SteelLabels = Plc.Settings.SteelSettings.SteelItems.Where(si => !string.IsNullOrEmpty(si.Name.Value))
            .Select(si => si.Name.Value).ToList();
    }

    private List<SingleMeasCell> GetSingleMeasuresListFromDiapasone(int diapasone)
    {
        return Plc.Settings.SingleMeasures[SelectedDiapasone].Measures.
            Where(c => c.IsChecked.Value).
            ToList();
    }
    [RelayCommand]
    private void DeleteSingleMeasureCell(object? o)
    {
        if (o is SingleMeasCell cell)
        {
            cell.IsChecked.WriteValue = false;
            PlcViewModel.MainPlcService.WriteParameter(cell.IsChecked);
        }
    }
    [RelayCommand]
    private async Task SaveToFileAsync()
    {
        
    }

}