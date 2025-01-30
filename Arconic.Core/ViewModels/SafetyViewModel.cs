using Arconic.Core.Infrastructure.DataContext.Data.Migrations;
using Arconic.Core.Models.PlcData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Arconic.Core.ViewModels;

public partial class SafetyViewModel:ObservableObject
{
    public SafetyViewModel(PlcViewModel plcViewModel)
    {
        Plc = plcViewModel.Plc;
        Init();
    }
    [ObservableProperty]
    private List<BoolSensorInfo>? _sensors;
    
    public Plc Plc { get; }

    private void Init()
    {
        Sensors =
        [
            // трубка
            Plc.Config.DiA7.Sensors[0],
            // генератор 1
            Plc.Config.DiA7.Sensors[1],
            // генератор 2
            Plc.Config.DiA7.Sensors[2],
            // рама
            Plc.Config.DiA7.Sensors[3],
            // детекторы
            Plc.Config.DiA7.Sensors[4]
        ];
    }
}