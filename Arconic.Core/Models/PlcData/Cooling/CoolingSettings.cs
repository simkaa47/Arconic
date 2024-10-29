using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Cooling;

public class CoolingSettings
{
    public Parameter<bool> SbPulsar { get; } =
        new Parameter<bool>("Измеритель расхода", false, true, DataType.DataBlock, 1, 1110, 0);
    public Parameter<bool> SbChiller { get; } =
        new Parameter<bool>("Питание чилера", 
            false, true, DataType.DataBlock, 1, 1110, 1);
    public Parameter<bool> SbPulsarEmulation { get; } =
        new Parameter<bool>("Эмуляция измерителя расхода", 
            false, true, DataType.DataBlock, 1, 1110, 2);
    public Parameter<bool> SqFlowTanksInEmulation { get; } =
        new Parameter<bool>("Эмуляция датчика потока на баки", 
            false, true, DataType.DataBlock, 1, 1110, 3);
    public Parameter<bool> SqFlowTanksOutEmulation { get; } =
        new Parameter<bool>("Эмуляция датчика потока c баков", 
            false, true, DataType.DataBlock, 1, 1110, 4);
    public Parameter<bool> SqFlowTubeOutEmulation { get; } =
        new Parameter<bool>("Эмуляция датчика потока c трубки", 
            false, true, DataType.DataBlock, 1, 1110, 5);
    public Parameter<bool> SqFlowTubeInEmulation { get; } =
        new Parameter<bool>("Эмуляция датчика потока на трубку", 
            false, true, DataType.DataBlock, 1, 1110, 6);
    public Parameter<bool> SqFlowTanksOutCabinetEmulation { get; } =
        new Parameter<bool>("Эмуляция датчика потока c баков в шкафу охлаждения", 
            false, true, DataType.DataBlock, 1, 1110, 7);
    public Parameter<bool> SqFlowTubeOutCabinetEmulation { get; } =
        new Parameter<bool>("Эмуляция датчика потока c трубки в шкафу охлаждения", 
            false, true, DataType.DataBlock, 1, 1111, 0);
    
    
    public Parameter<bool> SqFlowTanksInEmulationValue { get; } =
        new Parameter<bool>("Значение эмуляции датчика потока на баки", 
            false, true, DataType.DataBlock, 1, 1111, 1);
    public Parameter<bool> SqFlowTanksOutEmulationValue { get; } =
        new Parameter<bool>("Значение эмуляции датчика потока c баков", 
            false, true, DataType.DataBlock, 1, 1111, 2);
    public Parameter<bool> SqFlowTubeOutEmulationValue { get; } =
        new Parameter<bool>("Значение эмуляции датчика потока c трубки", 
            false, true, DataType.DataBlock, 1, 1111, 3);
    public Parameter<bool> SqFlowTubeInEmulationValue { get; } =
        new Parameter<bool>("Значение эмуляции датчика потока на трубку", 
            false, true, DataType.DataBlock, 1, 1111, 4);
    public Parameter<bool> SqFlowTanksOutCabinetEmulationValue { get; } =
        new Parameter<bool>("Значение эмуляции датчика потока c баков в шкафу охлаждения", 
            false, true, DataType.DataBlock, 1, 1111, 5);
    public Parameter<bool> SqFlowTubeOutCabinetEmulationValue { get; } =
        new Parameter<bool>("Значение эмуляции датчика потока c трубки в шкафу охлаждения", 
            false, true, DataType.DataBlock, 1, 1111, 6);

    public Parameter<float> PulsarEmulValue { get; } = new Parameter<float>("Измеритель расхода  - значение эмуляции",
        0, 100, DataType.DataBlock, 1, 1114);
    public Parameter<float> PulsarMinValue { get; } = new Parameter<float>("Измеритель расхода  - минимальное значение",
        0, 100, DataType.DataBlock, 1, 1118);
    public Parameter<float> PulsarMaxValue { get; } = new Parameter<float>("Измеритель расхода  - максимальное значение",
        0, 100, DataType.DataBlock, 1, 1122);
    public Parameter<float> ChillerSv { get; } = new Parameter<float>("Уставка температуры чиллера, С",
        0, 100, DataType.DataBlock, ParameterBase.SettingsDbNum, 1126);

}