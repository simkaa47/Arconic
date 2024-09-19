using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Measure;

public class MeasureIndicationAndControl
{
    #region Сброс ошибок

    public Parameter<bool> Rst { get; } =
        new Parameter<bool>("Сброс ошибок", false, true, DataType.DataBlock, 2, 1500, 0);

    #endregion

    public Parameter<float> Length { get; } =
        new Parameter<float>("Текущая длина, м", 0, float.MaxValue, DataType.DataBlock, 2, 7972);
    public Parameter<float> Width { get; } =
        new Parameter<float>("Текущая ширина, мм", 0, float.MaxValue, DataType.DataBlock, 2, 7976);
    public Parameter<float> Speed { get; } =
        new Parameter<float>("Текущая скорость, м/c", 0, float.MaxValue, DataType.DataBlock, 2, 7980);
    public Parameter<float> Thick { get; } =
        new Parameter<float>("Текущая толщина, мкм", 0, float.MaxValue, DataType.DataBlock, 2, 7992);
    public Parameter<ushort> ScanNumber { get; } =
        new Parameter<ushort>("Номер скана", 0, ushort.MaxValue, DataType.DataBlock, 2, 8000);
    
    public Parameter<float> CaretPosition { get; } =
        new Parameter<float>("Положение каретки от левого края полосы, мм", float.MinValue, float.MaxValue, DataType.DataBlock, 2, 8002);
    
    public Parameter<bool> StripUnderFlag { get; } =
        new Parameter<bool>("Полоса в измермтельном зазоре", false, true, DataType.DataBlock, 2, 1502, bitNum:0);
    
    public Parameter<short> MainStatuse { get; } =
        new Parameter<short>("Статус прибора", 0, 20, DataType.DataBlock, 2, 8006);
    public Parameter<float> Klin { get; } =
        new Parameter<float>("Клин, мкм", float.MinValue, float.MaxValue, DataType.DataBlock, 2, 8008);
    public Parameter<float> Chechevitsa { get; } =
        new Parameter<float>("Клин, мкм", float.MinValue, float.MaxValue, DataType.DataBlock, 2, 8012);

    public PlcScan PreviousScan { get; } = new PlcScan(1540);
    public PlcScan ActualScan { get; } = new PlcScan(4756);

    public SingleMeasureIndication SingleMeasureIndication { get; } = new SingleMeasureIndication();
}