using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.SteelLabel;

public class SteelMagazineItem(int dbNum, int offset, int index = 0)
{
    public int Index { get; } = index;
    public Parameter<string> Name { get; } =
        new Parameter<string>("Название марки стали", "", "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz", DataType.DataBlock, dbNum, offset)
            { Length = 26 };

    public Parameter<float> K1 { get; } =
        new Parameter<float>("Фактор сплава", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 26);
    
    public Parameter<float> K2 { get; } =
        new Parameter<float>("К-т марки стали 2", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 30);
    
    public Parameter<float> Si { get; } =
        new Parameter<float>("Массовая доля Si, %", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 34);
    public Parameter<float> Fe { get; } =
        new Parameter<float>("Массовая доля Fe, %", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 38);
    public Parameter<float> Cu { get; } =
        new Parameter<float>("Массовая доля Cu, %", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 42);
    public Parameter<float> Mn { get; } =
        new Parameter<float>("Массовая доля Mn, %", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 46);
    public Parameter<float> Mg { get; } =
        new Parameter<float>("Массовая доля Mg, %", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 50);
    public Parameter<float> Cr { get; } =
        new Parameter<float>("Массовая доля Cr, %", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 54);
    public Parameter<float> Zn { get; } =
        new Parameter<float>("Массовая доля Zn, %", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 58);
    public Parameter<float> Ni { get; } =
        new Parameter<float>("Массовая доля Ni, %", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 62);
    public Parameter<float> Ti { get; } =
        new Parameter<float>("Массовая доля Ti, %", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 66);
    public Parameter<float> Be { get; } =
        new Parameter<float>("Массовая доля Be, %", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 70);
    public Parameter<float> Ga { get; } =
        new Parameter<float>("Массовая доля Ga, %", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 74);
    public Parameter<float> Pb { get; } =
        new Parameter<float>("Массовая доля Pb, %", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 78);
    public Parameter<float> Cd { get; } =
        new Parameter<float>("Массовая доля Cd, %", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 82);
    public Parameter<float> Zr { get; } =
        new Parameter<float>("Массовая доля Zr, %", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 86);
    public Parameter<float> Li { get; } =
        new Parameter<float>("Массовая доля Li, %", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 90);
    public Parameter<float> Na { get; } =
        new Parameter<float>("Массовая доля Na, %", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 94);
    public Parameter<float> Co { get; } =
        new Parameter<float>("Массовая доля Co, %", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 98);
    public Parameter<float> V { get; } =
        new Parameter<float>("Массовая доля V, %", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 102);
    public Parameter<float> As { get; } =
        new Parameter<float>("Массовая доля As, %", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 106);
    public Parameter<float> Sc { get; } =
        new Parameter<float>("Массовая доля Sc, %", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 110);
    public Parameter<float> Ce { get; } =
        new Parameter<float>("Массовая доля Ce, %", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 114);
    

    
}