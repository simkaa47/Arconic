using Arconic.Core.Models.PlcData;
using Arconic.Core.Services.Plc;

namespace Arconic.Core.ViewModels;

public class PlcViewModel
{
    private readonly MainPlcService _mainPlcService;
    public Plc Plc { get; } = new Plc();

    public PlcViewModel(MainPlcService mainPlcService)
    {
        _mainPlcService = mainPlcService;
        InitAsync();
    }

    private async void InitAsync()
    {
        await _mainPlcService.ScanPlcAsync();
    }
}