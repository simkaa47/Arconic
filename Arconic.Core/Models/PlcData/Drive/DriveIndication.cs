using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Drive;

public class DriveIndication
{
    public Parameter<float> CurrentPosition { get; } = new Parameter<float>("Текущее положение рамы, мм",
        float.MinValue, float.MaxValue, DataType.DataBlock, 2, 200) { IsReadOnly = true };
    public Parameter<int> CurrentPositionPlc { get; } = new Parameter<int>("Положение рамы в импульсах энкодера",
        int.MinValue, int.MaxValue, DataType.DataBlock, 2, 204) { IsReadOnly = true };
    public Parameter<float> CurrentVelocity { get; } = new Parameter<float>("Текущая скорость, мм/c",
        float.MinValue, float.MaxValue, DataType.DataBlock, 2, 208) { IsReadOnly = true };
    
    public Parameter<uint> SetPosition { get; } = new Parameter<uint>("Позиция предустановки, мм",
        uint.MinValue, uint.MaxValue, DataType.DataBlock, 2, 216);

    public Parameter<bool> SbParking { get; } = new Parameter<bool>("Переместить раму в положение \"Парковка\"", false,
        true, DataType.DataBlock, 2, 220, 0);
    public Parameter<bool> SbMeasure { get; } = new Parameter<bool>("Переместить раму в положение \"Измерение\"", false,
        true, DataType.DataBlock, 2, 220,1);
    public Parameter<bool> SbJogForw { get; } = new Parameter<bool>("Шаговое перемещение вперед", false,
        true, DataType.DataBlock, 2, 220, 2);
    public Parameter<bool> SbJogRev { get; } = new Parameter<bool>("Шаговое перемещение назад", false,
        true, DataType.DataBlock, 2, 220,3);
    public Parameter<bool> SbRstErr { get; } = new Parameter<bool>("Сброс ошибки ПЧ", false,
        true, DataType.DataBlock, 2, 220,4);
    public Parameter<bool> SbSetPos { get; } = new Parameter<bool>("Установить положение рамы", false,
        true, DataType.DataBlock, 2, 220,5);
    public Parameter<short> DriveStatus { get; } =
        new Parameter<short>("Статус перемещения рамы", 0, 20, DataType.DataBlock, 2, 224);
    public Parameter<bool> IsParkingPosition { get; } =
        new Parameter<bool>("В позиции парковки",
            false, true, DataType.DataBlock, ParameterBase.IndicationDbNum, 222, 0);
    public Parameter<bool> IsCentralPosition { get; } =
        new Parameter<bool>("В позиции ЦЛ",
            false, true, DataType.DataBlock, ParameterBase.IndicationDbNum, 222, 1);
    public Parameter<bool> FcComm { get; } =
        new Parameter<bool>("Связь с ПЧ",
            false, true, DataType.DataBlock, ParameterBase.IndicationDbNum, 222, 4);
    
    public Parameter<bool> IsWaitPosition { get; } =
        new Parameter<bool>("В позиции ожидания полосы",
            false, true, DataType.DataBlock, ParameterBase.IndicationDbNum, 222, 7);
    
    public Parameter<bool> ErrSensorComm { get; } =
        new Parameter<bool>("Нет связи с датчиком положения рамы",
            false, true, DataType.DataBlock, ParameterBase.IndicationDbNum, 738, 4);
    
}