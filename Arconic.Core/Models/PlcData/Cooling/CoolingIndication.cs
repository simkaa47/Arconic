using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Cooling;

public class CoolingIndication
{
    public Parameter<bool> ChillerAlarmRelay { get; } =
        new Parameter<bool>("Аварийное реле чиллера", 
            false, true, DataType.DataBlock, 2, 710, 7);
    
    public Parameter<bool> ChillerErrComm { get; } =
        new Parameter<bool>("Ошибка связи с чиллером", 
            false, true, DataType.DataBlock, ParameterBase.IndicationDbNum, 711, 1);
    
    public Parameter<float> ChillerT1 { get; } =
        new Parameter<float>("Температура на входе чиллера, С", 
            float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.IndicationDbNum, 720){IsReadOnly = true};
    public Parameter<float> ChillerT2 { get; } =
        new Parameter<float>("Температура на выходе чиллера, С", 
            float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.IndicationDbNum, 724){IsReadOnly = true};

}