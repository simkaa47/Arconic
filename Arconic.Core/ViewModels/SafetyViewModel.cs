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
            new BoolSensorInfo(Plc.ControlAndIndication.SafetyIndication.SqHatchTube.Description,
                Plc.ControlAndIndication.SafetyIndication.SqHatchTube,
                Plc.Settings.SafetySettings.SqHatchTubeEmulValue,
                Plc.Settings.SafetySettings.SqHatchTubeEmulOnOff,
                Plc.ControlAndIndication.SafetyIndication.SqHatchTubeResult),
            // генератор 1
            new BoolSensorInfo(Plc.ControlAndIndication.SafetyIndication.SqGenerator1.Description,
                Plc.ControlAndIndication.SafetyIndication.SqGenerator1,
                Plc.Settings.SafetySettings.SqGeneratorEmulValue1,
                Plc.Settings.SafetySettings.SqGeneratorEmulOnOff1,
                Plc.ControlAndIndication.SafetyIndication.SqGeneratorResult1),
            // генератор 2
            new BoolSensorInfo(Plc.ControlAndIndication.SafetyIndication.SqGenerator2.Description,
                Plc.ControlAndIndication.SafetyIndication.SqGenerator2,
                Plc.Settings.SafetySettings.SqGeneratorEmulValue2,
                Plc.Settings.SafetySettings.SqGeneratorEmulOnOff2,
                Plc.ControlAndIndication.SafetyIndication.SqGeneratorResult2),
            // рама
            new BoolSensorInfo(Plc.ControlAndIndication.SafetyIndication.SqRack.Description,
                Plc.ControlAndIndication.SafetyIndication.SqRack,
                Plc.Settings.SafetySettings.SqRackEmulValue,
                Plc.Settings.SafetySettings.SqRackEmulOnOff,
                Plc.ControlAndIndication.SafetyIndication.SqRackResult),
            // детекторы
            new BoolSensorInfo(Plc.ControlAndIndication.SafetyIndication.SqDetectors.Description,
                Plc.ControlAndIndication.SafetyIndication.SqDetectors,
                Plc.Settings.SafetySettings.SqDetectorsEmulValue,
                Plc.Settings.SafetySettings.SqDetectorsEmulOnOff,
                Plc.ControlAndIndication.SafetyIndication.SqDetectorsResult)
            
        ];
    }
}