using CommunityToolkit.Mvvm.ComponentModel;

namespace Arconic.Core.ViewModels;

public partial class MainViewModel(PlcViewModel plcViewModel) : ObservableObject
{
    public PlcViewModel PlcViewModel { get; } = plcViewModel;
}