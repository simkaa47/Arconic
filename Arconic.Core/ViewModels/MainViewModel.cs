using Arconic.Core.Services.Plc;

namespace Arconic.Core.ViewModels;

public class MainViewModel
{
    public MainPlcService MainPlcService { get; }

    public MainViewModel(MainPlcService mainPlcService)
    {
        MainPlcService = mainPlcService;
        InitAsync();
    }

    private async void InitAsync()
    {
        await MainPlcService.ScanPlcAsync();
    }
}