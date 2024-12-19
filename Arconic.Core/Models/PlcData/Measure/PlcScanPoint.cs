using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Measure;

public class PlcScanPoint(int offset)
{
    public Parameter<float> Position { get; } = 
        new("Положение, мм", float.MinValue, float.MaxValue, DataType.DataBlock, 83, offset);
    public Parameter<float> Thick { get; } = 
        new("Толщина, мкм", float.MinValue, float.MaxValue, DataType.DataBlock, 83, offset+4);
    public Parameter<float> Length { get; } = 
        new("Длина, м", float.MinValue, float.MaxValue, DataType.DataBlock, 83, offset+8);
    
    
}