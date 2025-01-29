using Arconic.Core.Models.PlcData;
using Arconic.Core.Models.PlcModules;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Arconic.Core.ViewModels;

public partial class PlcConfigurationViewModel(PlcViewModel plcViewModel) : ObservableObject
{
    public Plc Plc { get; } = plcViewModel.Plc;
    [ObservableProperty]
    private IPlcModule? _selectedModule = plcViewModel.Plc.Config.Modules.FirstOrDefault();
}