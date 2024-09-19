using Arconic.Core.Models.PlcData;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;

namespace Arconic.Core.ViewModels;

public partial class SingleMeasuresViewModel:ObservableObject
{
    private readonly ILogger<SingleMeasuresViewModel> _logger;

    public SingleMeasuresViewModel(ILogger<SingleMeasuresViewModel> logger, PlcViewModel plcViewModel)
    {
        _logger = logger;
        Plc = plcViewModel.Plc;
        Init();
    }

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
    }   
    
    [ObservableProperty]
    private IEnumerable<string?> _steelLabels;


    private void GetSteelLabels()
    {
        SteelLabels = Plc.Settings.SteelSettings.SteelItems.Where(si => !string.IsNullOrEmpty(si.Name.Value))
            .Select(si => si.Name.Value).ToList();
    }

}