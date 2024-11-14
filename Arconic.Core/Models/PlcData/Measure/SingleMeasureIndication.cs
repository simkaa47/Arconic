using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Measure;

public class SingleMeasureIndication
{
    public Parameter<float> MeasuredThick { get; } = new Parameter<float>("Измеренная толщина, мкм", float.MinValue,
        float.MaxValue, DataType.DataBlock, 2, 1504);
    public Parameter<float> NominalThick { get; } = new Parameter<float>("Номинальная толщина, мкм", 0,
        float.MaxValue, DataType.DataBlock, 2, 1508);
    public Parameter<string> SteelLabel { get; } = new Parameter<string>("Марка стали", "",
        "zzzzzzzzzzzzzzzz", DataType.DataBlock, 2, 1512){Length = 26};
    public Parameter<short> CurrentTime { get; } = new Parameter<short>("Текущее время измерения, с", 0,
        short.MaxValue, DataType.DataBlock, 2, 1538){IsReadOnly = true};
    public Parameter<bool> SingleMeasFlag { get; } =
        new Parameter<bool>("Флаг единичного измерения", false, true, DataType.DataBlock, 2, 1502, 3);
    public Parameter<bool> SingleMeasCmd { get; } =
        new Parameter<bool>("Провести единичное измерение", false, true, DataType.DataBlock, 2, 1500, 3);
    public Parameter<bool> SingleMeasCancel { get; } =
        new Parameter<bool>("Отменить единичное измерение", false, true, DataType.DataBlock, 2, 1500, 4);
    
    public Parameter<bool> AddSingleMeas { get; } =
        new Parameter<bool>("Добавить единичное измерение", false, true, DataType.DataBlock, 2, 1500, 6);
    
}