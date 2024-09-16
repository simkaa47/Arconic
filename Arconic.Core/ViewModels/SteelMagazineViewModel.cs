using Arconic.Core.Models.PlcData;
using Arconic.Core.Models.PlcData.SteelLabel;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;

namespace Arconic.Core.ViewModels;

public partial class SteelMagazineViewModel(PlcViewModel plcViewModel, ILogger<SteelMagazineViewModel> logger)
    : ObservableObject
{
    private readonly PlcViewModel _plcViewModel = plcViewModel;
    private readonly ILogger<SteelMagazineViewModel> _logger = logger;
    public Plc Plc { get; } = plcViewModel.Plc;
    [ObservableProperty]
    private SteelMagazineItem? _selectedSteel;




}