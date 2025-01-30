using Arconic.Core.Models.PlcData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Arconic.Core.ViewModels;

public partial class CoolingViewModel:ObservableObject
{
    private readonly PlcViewModel _plcViewModel;
    public Plc Plc { get; }

    public CoolingViewModel(PlcViewModel plcViewModel)
    {
        _plcViewModel = plcViewModel;
        Plc = plcViewModel.Plc;
        Init();
    }
    [ObservableProperty]
    private List<BoolSensorInfo>? _flowBoolSensors;

    private void Init()
    {
        FlowBoolSensors =
        [
            //Датчик потока на выходе бака
            Plc.Config.DiA6.Sensors[6],
            // Датчик потока c баков (гидрошкаф)
            Plc.Config.DiA8.Sensors[2],
            // Датчик потока c баков (гидрошкаф)
            Plc.Config.DiA8.Sensors[3]
        ];
    }
}