using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Detectors;

public class StandDiap
{
    public StandDiap(int startByte)
    {
        StartByte = startByte;
        Close = Enumerable.Range(0, DetectorsSettings.DetectorsNum).Select(i =>
                new Parameter<float>($"Детектор №{i + 1}", 0, ushort.MaxValue, DataType.DataBlock, 1, StartByte+i*4, 0))
            .ToArray();
        Open = Enumerable.Range(0, DetectorsSettings.DetectorsNum).Select(i =>
                new Parameter<float>($"Детектор №{i + 1}", 0, ushort.MaxValue, DataType.DataBlock, 1, StartByte+4*DetectorsSettings.DetectorsNum+i*4, 0))
            .ToArray();
    }

    private int StartByte { get;}
    public Parameter<float>[] Close { get; }
    public Parameter<float>[] Open { get; }
}