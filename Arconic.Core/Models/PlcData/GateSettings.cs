using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData;

public class GateSettings(int offset)
{
    public Parameter<float> HighLimit { get; } =
        new("Верхний предел", 0, float.MaxValue, DataType.DataBlock, 1, offset, 0);
    public Parameter<bool> Gate1 { get; } =
        new("Дополнительный затвор 1", false, true, DataType.DataBlock, 1, offset+4, 0);
    public Parameter<bool> Gate2 { get; } =
        new("Дополнительный затвор 2", false, true, DataType.DataBlock, 1, offset+4, 1);
}