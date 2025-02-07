using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Drive;

public class DriveSettings
{
    public Parameter<ushort> SpeedHz { get; } =
        new Parameter<ushort>("Скорость перемещения рамы, мм/c", 0, 220, DataType.DataBlock, 1, 1130);

    public Parameter<int> ParkingPosition { get; } =
        new Parameter<int>("Позиция паркинга, мм", int.MinValue, int.MaxValue, DataType.DataBlock, 1, 1132);
    
    public Parameter<int> WaitPosition { get; } =
        new Parameter<int>("Позиция ожидания рамы рядом со станом, мм", int.MinValue, int.MaxValue, DataType.DataBlock, 1, 1154);

    public Parameter<int> CentralPosition { get; } =
        new Parameter<int>("Позиция ЦЛ, мм", int.MinValue, int.MaxValue, DataType.DataBlock, 1, 1136);

    public Parameter<int> MaxMeasureWidth { get; } =
        new Parameter<int>("Максимальная ширина, мм", 0, int.MaxValue, DataType.DataBlock, 1, 1140);

    public Parameter<float> Kgear { get; } =
        new Parameter<float>("К-т редукции", 0, float.MaxValue, DataType.DataBlock, 1, 1148);

    public Parameter<short> MeasMode { get; } =
        new Parameter<short>("Режим измерения", 0, 2, DataType.DataBlock, 1, 1152);
}