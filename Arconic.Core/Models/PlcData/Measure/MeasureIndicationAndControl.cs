using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Measure;

public class MeasureIndicationAndControl
{
    #region Сброс ошибок
    public Parameter<bool> Rst { get; } =
        new Parameter<bool>("Сброс ошибок", false, true, DataType.DataBlock, 2, 1500, 0);

    #endregion
    #region Стандартизация
    public Parameter<bool> SbStandartisation { get; } =
        new Parameter<bool>("Провести стандартизацию", false, true, DataType.DataBlock, 2, 1500, 1);
    
    public Parameter<bool> StandartisationFlag { get; } =
        new Parameter<bool>("Флаг стандартизации", false, true, DataType.DataBlock, 2, 1502, 2);

    #endregion
    #region Новая полоса
    public Parameter<bool> SbNewStrip { get; } =
        new Parameter<bool>("Новая полоса", false, true, DataType.DataBlock, 2, 1500, 2);

    #endregion
    #region Режим калибровки
    public Parameter<bool> SbCalibration { get; } =
        new Parameter<bool>("Режим калибровки", false, true, DataType.DataBlock, 2, 1500, 5);

    #endregion

    public Parameter<float> Length { get; } =
        new Parameter<float>("Текущая длина, м", 0, float.MaxValue, DataType.DataBlock, 2, 7972);
    public Parameter<float> Width { get; } =
        new Parameter<float>("Текущая ширина, мм", 0, float.MaxValue, DataType.DataBlock, 2, 7976);
    public Parameter<float> Speed { get; } =
        new Parameter<float>("Текущая скорость, м/мин", 0, float.MaxValue, DataType.DataBlock, 2, 7980);
    public Parameter<float> Thick { get; } =
        new Parameter<float>("Текущая толщина, мкм", 0, float.MaxValue, DataType.DataBlock, 2, 7992);
    
    public Parameter<float> ArmThick { get; } =
        new Parameter<float>("Текущая толщина, мкм", 0, float.MaxValue, DataType.DataBlock, 2, 8022);
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

    public Parameter<ushort> CloseGateTimeCv { get; } = new Parameter<ushort>("Осталось времени до закрытия затвора, с",
        0, ushort.MaxValue, DataType.DataBlock, ParameterBase.IndicationDbNum, 8020){IsReadOnly = true};

    public SingleMeasureIndication SingleMeasureIndication { get; } = new SingleMeasureIndication();
    
    public Parameter<float> ThickDeviation { get; } =
        new Parameter<float>("Отклонение толщины, мкм", float.MinValue, float.MaxValue, DataType.DataBlock, 2, 8026){IsReadOnly = true};
    
    public Parameter<short> AoThickDeviationAdc { get; } =
        new Parameter<short>("Значение АЦП", short.MinValue, short.MaxValue, DataType.DataBlock, 2, 8030){IsReadOnly = true};
    
    public Parameter<float> AoThickDeviationVoltage { get; } =
        new Parameter<float>("Напряжение аналогового выхода, В", float.MinValue, float.MaxValue, DataType.DataBlock, 2, 8032)
            {IsReadOnly = true};

    public List<BoolSensorInfo> BoolValues { get; } =
    [
        // Управление и индикация сигнала "Металл в клети"
        new BoolSensorInfo(
        "Металл в клети",
        new Parameter<bool>("Дискретный вход 'Металл в клети'", false, true, DataType.Input, 0, 1),
        new Parameter<bool>("Значение эмуляции сигнала 'Металл в клети'", false, true, DataType.DataBlock, 2, 1501),
        new Parameter<bool>("Эмуляция сигнала 'Металл в клети'", false, true, DataType.DataBlock, 2, 1500, 7),
        new Parameter<bool>("Результат сигнала 'Металл в клети'", false, true, DataType.DataBlock, 2, 1502, 4))
    ];
    
    
   

}