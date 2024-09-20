using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Cooling;

public class CoolingIndication
{
    public Parameter<bool> PulsarComm { get; } =
        new Parameter<bool>("Связь с измерителем расхода", 
            false, true, DataType.DataBlock, 2, 710, 0);
    public Parameter<bool> SqFlowTanksInResult { get; } =
        new Parameter<bool>("Датчик потока на баки (результат)", 
            false, true, DataType.DataBlock, 2, 710, 1);
    public Parameter<bool> SqFlowTanksOutResult { get; } =
        new Parameter<bool>("Датчик потока c баков (результат)", 
            false, true, DataType.DataBlock, 2, 710, 2);
    public Parameter<bool> SqFlowTubeOutResult { get; } =
        new Parameter<bool>("Датчик потока c трубки (результат)", 
            false, true, DataType.DataBlock, 2, 710, 3);
    public Parameter<bool> SqFlowTubeInResult { get; } =
        new Parameter<bool>("Датчик потока на трубку (результат)", 
            false, true, DataType.DataBlock, 2, 710, 4);
    public Parameter<bool> SqFlowTanksOutCabinetResult { get; } =
        new Parameter<bool>("Датчик потока c баков в шкафу охлаждения (результат)", 
            false, true, DataType.DataBlock, 2, 710, 5);
    public Parameter<bool> SqFlowTubeOutCabinetResult { get; } =
        new Parameter<bool>("Датчик потока c трубки в шкафу охлаждения (результат)", 
            false, true, DataType.DataBlock, 2, 710, 6);
    public Parameter<bool> ChillerAlarmRelay { get; } =
        new Parameter<bool>("Аварийное реле чиллера", 
            false, true, DataType.DataBlock, 2, 710, 7);
    public Parameter<bool> ChillerContactor { get; } =
        new Parameter<bool>("Контактор чиллера", 
            false, true, DataType.DataBlock, 2, 711, 0);

    public Parameter<float> PulsarValue { get; } =
        new Parameter<float>("Фактическое значение измерителя расхода", 
            float.MinValue, float.MaxValue, DataType.DataBlock, 2, 716){IsReadOnly = true};
    public Parameter<float> PulsarValueResult { get; } =
        new Parameter<float>("Результирующее значение измерителя расхода", 
            float.MinValue, float.MaxValue, DataType.DataBlock, 2, 712){IsReadOnly = true};
    
    public Parameter<bool> SqFlowTanksIn { get; } =
        new Parameter<bool>("Датчик потока на баки", 
            false, true, DataType.Input, 0, 12, 5);
    public Parameter<bool> SqFlowTanksOut { get; } =
        new Parameter<bool>("Датчик потока c баков", 
            false, true, DataType.Output, 0, 12, 6);
    public Parameter<bool> SqFlowTubeOut { get; } =
        new Parameter<bool>("Датчик потока c трубки", 
            false, true, DataType.Input, 0, 12, 7);
    public Parameter<bool> SqFlowTubeIn { get; } =
        new Parameter<bool>("Датчик потока на трубку", 
            false, true, DataType.Input, 0, 13, 0);
    public Parameter<bool> SqFlowTanksOutCabinet { get; } =
        new Parameter<bool>("Датчик потока c баков в шкафу охлаждения", 
            false, true, DataType.Input, 0, 16, 2);
    public Parameter<bool> SqFlowTubeOutCabinet { get; } =
        new Parameter<bool>("Датчик потока c трубки в шкафу охлаждения", 
            false, true, DataType.Input, 0, 16, 3);
    
    

}