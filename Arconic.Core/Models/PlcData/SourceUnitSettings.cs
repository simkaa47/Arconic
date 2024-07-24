using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData;

public class SourceUnitSettings()
{
    public GateSettings[] GateSettings { get; } =
        Enumerable.Range(0, 3).Select(i => new GateSettings(i * 10)).ToArray();

    public Parameter<ushort> VoltageSv { get; } =
        new Parameter<ushort>("Уставка напряжения, кВ", 0, 1600, DataType.DataBlock, 1, 30, 0);
    public Parameter<ushort> CurrentSv { get; } =
        new Parameter<ushort>("Уставка тока, mA", 0, 300, DataType.DataBlock, 1, 32, 0);
    public Parameter<ushort> TimeoutSqGate { get; } =
        new Parameter<ushort>("Тайм-аут срабатывания датчика затвора, с", 0, 300, DataType.DataBlock, 1, 34, 0);
    public Parameter<ushort> TimeOutComm { get; } =
        new Parameter<ushort>("Время определения связи с генератором, с", 0, 300, DataType.DataBlock, 1, 36, 0);
    public Parameter<bool> SbPower { get; } =
        new Parameter<bool>("Кнопка подачи питания на генератор", false, true, DataType.DataBlock, 1, 48, 0);
    public Parameter<bool> SbHv { get; } =
        new Parameter<bool>("Кнопка включения высокого напряжения", false, true, DataType.DataBlock, 1, 48, 2);
    public Parameter<bool> SbMa { get; } =
        new Parameter<bool>("Кнопка включения тока", false, true, DataType.DataBlock, 1, 48, 3);
    public Parameter<bool> SbGate { get; } =
        new Parameter<bool>("Кнопка управления затвором", false, true, DataType.DataBlock, 1, 48, 4);
    public Parameter<bool> SbCond { get; } =
        new Parameter<bool>("Кнопка включения кондиционирования", false, true, DataType.DataBlock, 1, 48, 5);
    public Parameter<bool> SbImitation { get; } =
        new Parameter<bool>("Имитация генератора", false, true, DataType.DataBlock, 1, 48, 6);
    public Parameter<bool> SbGate1 { get; } =
        new Parameter<bool>("Кнопка управления дополнительным затвором 1", false, true, DataType.DataBlock, 1, 48, 7);
    public Parameter<bool> SbGate2 { get; } =
        new Parameter<bool>("Кнопка управления дополнительным затвором 2", false, true, DataType.DataBlock, 1, 49, 0);
}