using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Console.WriteAbsorbtions;

public class ElementInfo(int dbNum, int byteOffset)
{
    public Parameter<string> Name { get; } = 
        new Parameter<string>("","","zzzz", DataType.DataBlock, dbNum, byteOffset) {Length = 4};
    public Parameter<float> Density { get; } = 
        new Parameter<float>("", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, byteOffset+6);

    public List<AbsorbtionInfo> AbsorbtionInfos { get; } =
        Enumerable.Range(0, 60).Select(i => new AbsorbtionInfo(dbNum, byteOffset+10 + i * 16)).ToList();


}