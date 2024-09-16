using Arconic.Core.Abstractions.SteelMagazine;
using Arconic.Core.Models.PlcData;
using Arconic.Core.Models.PlcData.SteelLabel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;

namespace Arconic.Core.ViewModels;

public partial class SteelMagazineViewModel(PlcViewModel plcViewModel, 
    ILogger<SteelMagazineViewModel> logger, 
    ISteelMagazineDialog steelMagazineDialog)
    : ObservableObject
{
    private readonly PlcViewModel _plcViewModel = plcViewModel;
    private readonly ILogger<SteelMagazineViewModel> _logger = logger;
    private readonly ISteelMagazineDialog _steelMagazineDialog = steelMagazineDialog;
    public Plc Plc { get; } = plcViewModel.Plc;
    [ObservableProperty]
    private SteelMagazineItem? _selectedSteel;
    
    [RelayCommand]
    private async Task AddSteelAsync()
    {
        if (await _steelMagazineDialog.ShowDialogAsync())
        {
            
        }
    }
    
    [RelayCommand]
    private async Task DeleteSteelAsync()
    {
        
    }


}