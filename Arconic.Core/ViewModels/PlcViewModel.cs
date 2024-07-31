using Arconic.Core.Models.Parameters;
using Arconic.Core.Models.PlcData;
using Arconic.Core.Services.Plc;
using CommunityToolkit.Mvvm.Input;

namespace Arconic.Core.ViewModels;

public partial class PlcViewModel
{
    public MainPlcService MainPlcService { get; }
    public Plc Plc { get; } = new Plc();

    public PlcViewModel(MainPlcService mainPlcService)
    {
        MainPlcService = mainPlcService;
        InitAsync();
    }
    private async void  InitAsync()
    {
        await MainPlcService.ScanPlcAsync();
    }

    [RelayCommand]
    private void WriteParameter(object o)
    {
        if (o is ParameterBase par)
        {
            MainPlcService.WriteParameter(par);
        }
    }
}