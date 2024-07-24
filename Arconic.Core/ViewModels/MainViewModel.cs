using Arconic.Core.Services.Plc;

namespace Arconic.Core.ViewModels;

public class MainViewModel(PlcViewModel plcViewModel)
{
    public PlcViewModel PlcViewModel { get; } = plcViewModel;
}