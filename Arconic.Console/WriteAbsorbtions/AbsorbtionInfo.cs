using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Console.WriteAbsorbtions;

public class AbsorbtionInfo(int dbNum, int byteOffset)
{
    public Parameter<double> EnergyLevel { get; } =
        new Parameter<double>("", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, byteOffset);
    public Parameter<double> AbsCoeff { get; } =
        new Parameter<double>("", float.MinValue, float.MaxValue, DataType.DataBlock, dbNum, byteOffset+8);
}