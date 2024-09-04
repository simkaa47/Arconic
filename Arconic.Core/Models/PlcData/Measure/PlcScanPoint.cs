using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Measure;

public class PlcScanPoint(int offset)
{
    public Parameter<float> Position { get; } = 
        new("Положение, мм", float.MinValue, float.MaxValue, DataType.DataBlock, 2, offset);
    public Parameter<float> Thick { get; } = 
        new("Толщина, мкм", float.MinValue, float.MaxValue, DataType.DataBlock, 2, offset+4);
}