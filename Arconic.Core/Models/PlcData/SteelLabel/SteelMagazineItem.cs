using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.SteelLabel;

public class SteelMagazineItem(int dbNum, int offset)
{
    public Parameter<string> Name { get; } =
        new Parameter<string>("", "", "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz", DataType.DataBlock, dbNum, offset)
            { Length = 26 };

    public Parameter<float> K1 { get; } =
        new Parameter<float>("", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 26);
    
    public Parameter<float> K2 { get; } =
        new Parameter<float>("", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, offset + 30);

    public List<Parameter<float>> Chemistry { get; } = Enumerable.Range(0, 31)
        .Select(i => new Parameter<float>("", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, i * 4 + 34))
        .ToList();
}