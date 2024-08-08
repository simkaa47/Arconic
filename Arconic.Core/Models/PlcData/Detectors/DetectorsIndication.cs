using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Detectors;

public class DetectorsIndication
{
    public Parameter<float>[] Values { get; } = Enumerable.Range(0, 32).Select(i =>
        new Parameter<float>($"Детектор №{i + 1}", float.MinValue, float.MaxValue, DataType.DataBlock, 2, 50 + i * 4,
            0)).ToArray();
}