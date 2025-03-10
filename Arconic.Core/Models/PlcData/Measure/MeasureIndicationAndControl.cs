﻿using Arconic.Core.Models.Parameters;
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
        new Parameter<float>("Текущая длина, м", 0, float.MaxValue, DataType.DataBlock, 2, 1540);
    public Parameter<float> Width { get; } =
        new Parameter<float>("Текущая ширина, мм", 0, float.MaxValue, DataType.DataBlock, 2, 1544);
    public Parameter<float> Speed { get; } =
        new Parameter<float>("Текущая скорость, м/мин", 0, float.MaxValue, DataType.DataBlock, 2, 1548);
    public Parameter<float> Thick { get; } =
        new Parameter<float>("Текущая толщина, мкм", 0, float.MaxValue, DataType.DataBlock, 2, 1560);
    
    public Parameter<float> ArmThick { get; } =
        new Parameter<float>("Текущая толщина, мкм", 0, float.MaxValue, DataType.DataBlock, 2, 1590);
    public Parameter<ushort> ScanNumber { get; } =
        new Parameter<ushort>("Номер скана", 0, ushort.MaxValue, DataType.DataBlock, 2, 1568);
    
    public Parameter<float> CaretPosition { get; } =
        new Parameter<float>("Положение каретки от левого края полосы, мм", float.MinValue, float.MaxValue, DataType.DataBlock, 2, 1570);
    
    public Parameter<bool> StripUnderFlag { get; } =
        new Parameter<bool>("Полоса в измермтельном зазоре", false, true, DataType.DataBlock, 2, 1502, bitNum:0);
    
    public Parameter<short> MainStatuse { get; } =
        new Parameter<short>("Статус прибора", 0, 20, DataType.DataBlock, 2, 1574);
    public Parameter<float> Klin { get; } =
        new Parameter<float>("Клин, мкм", float.MinValue, float.MaxValue, DataType.DataBlock, 2, 1576);
    public Parameter<float> Chechevitsa { get; } =
        new Parameter<float>("Чечевица, мкм", float.MinValue, float.MaxValue, DataType.DataBlock, 2, 1580);
    
    public Parameter<float> KlinRelative { get; } =
        new Parameter<float>("Клин, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, 1616);
    public Parameter<float> ChechevitsaRelative { get; } =
        new Parameter<float>("Чечевица, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, 1620);
    
    public Parameter<float> ChechevitsaAverage { get; } =
        new Parameter<float>("Средняя чечечвица за полосу, мкм", float.MinValue, float.MaxValue, DataType.DataBlock, 2, 1628);
    public Parameter<float> KlinAverage { get; } =
        new Parameter<float>("Средний клин за полосу, мкм", float.MinValue, float.MaxValue, DataType.DataBlock, 2, 1624);
    public Parameter<float> ChechevitsaAverageRelative { get; } =
        new Parameter<float>("Средняя чечечвица за полосу, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, 1636);
    public Parameter<float> KlinAverageRelative { get; } =
        new Parameter<float>("Средний клин за полосу, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, 1632);

    public PlcScan PreviousScan { get; } = new PlcScan(0);
    public PlcScan ActualScan { get; } = new PlcScan(7224);

    public Parameter<ushort> CloseGateTimeCv { get; } = new Parameter<ushort>("Осталось времени до закрытия затвора, с",
        0, ushort.MaxValue, DataType.DataBlock, ParameterBase.IndicationDbNum, 1588){IsReadOnly = true};

    public SingleMeasureIndication SingleMeasureIndication { get; } = new SingleMeasureIndication();
    
    public Parameter<float> ThickDeviation { get; } =
        new Parameter<float>("Отклонение толщины, мкм", float.MinValue, float.MaxValue, DataType.DataBlock, 2, 1594){IsReadOnly = true};
    
    public Parameter<short> AoThickDeviationAdc { get; } =
        new Parameter<short>("Значение АЦП", short.MinValue, short.MaxValue, DataType.DataBlock, 2, 1598){IsReadOnly = true};
    
    public Parameter<float> AoThickDeviationVoltage { get; } =
        new Parameter<float>("Напряжение аналогового выхода, В", float.MinValue, float.MaxValue, DataType.DataBlock, 2, 1600)
            {IsReadOnly = true};

}