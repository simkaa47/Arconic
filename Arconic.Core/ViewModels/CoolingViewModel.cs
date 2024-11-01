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
            //Датчик потока на входе в бак
            /*new BoolSensorInfo(Plc.ControlAndIndication.CoolingIndication.SqFlowTanksIn.Description, 
                Plc.ControlAndIndication.CoolingIndication.SqFlowTanksIn,
                 Plc.Settings.CoolingSettings.SqFlowTanksInEmulationValue,
                Plc.Settings.CoolingSettings.SqFlowTanksInEmulation,
                Plc.ControlAndIndication.CoolingIndication.SqFlowTanksInResult),*/
            //Датчик потока на выходе бака
            new BoolSensorInfo(Plc.ControlAndIndication.CoolingIndication.SqFlowTanksOut.Description, 
                Plc.ControlAndIndication.CoolingIndication.SqFlowTanksOut,
                Plc.Settings.CoolingSettings.SqFlowTanksOutEmulationValue,
                Plc.Settings.CoolingSettings.SqFlowTanksOutEmulation,
                Plc.ControlAndIndication.CoolingIndication.SqFlowTanksOutResult),
            // Датчик потока на входе трубок
            /* new BoolSensorInfo(Plc.ControlAndIndication.CoolingIndication.SqFlowTubeIn.Description, 
                Plc.ControlAndIndication.CoolingIndication.SqFlowTubeIn,
                Plc.Settings.CoolingSettings.SqFlowTubeInEmulationValue,
                Plc.Settings.CoolingSettings.SqFlowTubeInEmulation,
                Plc.ControlAndIndication.CoolingIndication.SqFlowTubeInResult),
            // Датчик потока на выходе трубок
            new BoolSensorInfo(Plc.ControlAndIndication.CoolingIndication.SqFlowTubeOut.Description, 
                Plc.ControlAndIndication.CoolingIndication.SqFlowTubeOut,
                Plc.Settings.CoolingSettings.SqFlowTubeOutEmulationValue,
                Plc.Settings.CoolingSettings.SqFlowTubeOutEmulation,
                Plc.ControlAndIndication.CoolingIndication.SqFlowTubeOutResult),*/
            // Датчик потока c баков (гидрошкаф)
            new BoolSensorInfo(Plc.ControlAndIndication.CoolingIndication.SqFlowTanksOutCabinet.Description, 
                Plc.ControlAndIndication.CoolingIndication.SqFlowTanksOutCabinet,
                Plc.Settings.CoolingSettings.SqFlowTanksOutCabinetEmulationValue,
                Plc.Settings.CoolingSettings.SqFlowTanksOutCabinetEmulation,
                Plc.ControlAndIndication.CoolingIndication.SqFlowTanksOutCabinetResult),
            // Датчик потока c баков (гидрошкаф)
            new BoolSensorInfo(Plc.ControlAndIndication.CoolingIndication.SqFlowTubeOutCabinet.Description, 
                Plc.ControlAndIndication.CoolingIndication.SqFlowTubeOutCabinet,
                Plc.Settings.CoolingSettings.SqFlowTubeOutCabinetEmulationValue,
                Plc.Settings.CoolingSettings.SqFlowTubeOutCabinetEmulation,
                Plc.ControlAndIndication.CoolingIndication.SqFlowTubeOutCabinetResult),
            ];
    }
}