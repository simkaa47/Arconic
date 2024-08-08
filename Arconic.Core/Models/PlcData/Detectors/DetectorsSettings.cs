using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Detectors;

public class DetectorsSettings
{
    public const int DetectorsNum = 32;
    public StandDiap Diap0 { get; } = new StandDiap(52);
    public StandDiap Diap1 { get; } = new StandDiap(308);
    public StandDiap Diap2 { get; } = new StandDiap(564);

    public Parameter<bool> EmulOnOff { get; } =
        new Parameter<bool>("Эмуляция значений детекторов", false, true, DataType.DataBlock, 1, 50, 0);

    public Parameter<ushort> KGain { get; } =
        new Parameter<ushort>("К-т ослабления", 1, 7, DataType.DataBlock, 1, 820, 0);
    public Parameter<ushort> ExpTime { get; } =
        new Parameter<ushort>("Время экспозиции, мкс", 0, 65535, DataType.DataBlock, 1, 822, 0);

    public Parameter<ushort>[] EmulationValues { get; } = Enumerable.Range(0, 32).Select(i =>
            new Parameter<ushort>($"Детектор №{i + 1}", 0, ushort.MaxValue, DataType.DataBlock, 1, 828 + i * 2, 0))
        .ToArray();

}