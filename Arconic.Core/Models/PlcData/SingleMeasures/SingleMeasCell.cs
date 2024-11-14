using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.SingleMeasures;

public class SingleMeasCell(int offset)
{
    public Parameter<DateTime> DateAndTime { get; } = new Parameter<DateTime>("Дата и время измерения",
        DateTime.MinValue, 
        DateTime.MaxValue, 
        DataType.DataBlock, 
        ParameterBase.SettingsDbNum, 
        offset);
    public Parameter<float> Weak { get; } = new Parameter<float>("Ослабление",
        float.MinValue,
        float.MaxValue,
        DataType.DataBlock,
        ParameterBase.SettingsDbNum,
        offset + 8);

    public Parameter<float> Thick { get; } = new Parameter<float>("Толщина", 
        float.MinValue, 
        float.MaxValue,
        DataType.DataBlock, 
        ParameterBase.SettingsDbNum, 
        offset + 12);

    public Parameter<bool> IsChecked { get; } = new Parameter<bool>("", false, true, DataType.DataBlock,
        ParameterBase.SettingsDbNum, offset + 16);

}