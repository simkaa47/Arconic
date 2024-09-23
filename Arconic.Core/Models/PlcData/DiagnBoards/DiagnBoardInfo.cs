using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.DiagnBoards;

public class DiagnBoardInfo(int settOffset, int indOffset, string boardName)
{
    public string Name { get; } = boardName;
    // Опрос платы
    public Parameter<bool> OnOff { get; } = new Parameter<bool>($"{boardName} - Опрос платы",
        false, true, DataType.DataBlock, ParameterBase.SettingsDbNum, settOffset, 0);
    // Опрос платы
    public Parameter<bool> ShockDetecting { get; } = new Parameter<bool>($"{boardName} - Детектирование удара",
        false, true, DataType.DataBlock, ParameterBase.SettingsDbNum, settOffset, 3);
    // Текущее ускорение по оси X
    public Parameter<short> AccAxisX { get; } = new Parameter<short>($"{boardName} - Текущее ускорение по оси X",
        short.MinValue, short.MaxValue, DataType.DataBlock, ParameterBase.IndicationDbNum, indOffset+12){IsReadOnly = true};
    // Текущее ускорение по оси Y
    public Parameter<short> AccAxisY { get; } = new Parameter<short>($"{boardName} - Текущее ускорение по оси Y",
        short.MinValue, short.MaxValue, DataType.DataBlock, ParameterBase.IndicationDbNum, indOffset+14){IsReadOnly = true};
    // Текущее ускорение по оси Z
    public Parameter<short> AccAxisZ { get; } = new Parameter<short>($"{boardName} - Текущее ускорение по оси Z",
        short.MinValue, short.MaxValue, DataType.DataBlock, ParameterBase.IndicationDbNum, indOffset+16){IsReadOnly = true};
    // Дата последнего удара
    public Parameter<DateTime> LastShockDate { get; } = new Parameter<DateTime>($"{boardName} - Дата последнего удара",
        DateTime.MinValue, DateTime.MaxValue, DataType.DataBlock, ParameterBase.IndicationDbNum, indOffset+50){IsReadOnly = true};
    // Связь с платой
    public Parameter<bool> ErrCom { get; } = new Parameter<bool>($"{boardName} - Связь с платой",
        false, true, DataType.DataBlock, ParameterBase.IndicationDbNum, indOffset+58, 0);
    
    
    
    // Температура и влажность
    public List<AnalogSensorInfo> AnalogSensorInfos { get; } = [
        new AnalogSensorInfo("Температура", new Parameter<short>($"{boardName} - Значение АЦП температуры", 0, 32767, DataType.DataBlock, 2, indOffset){IsReadOnly = true},
            new Parameter<float>($"{boardName} - Температура", float.MinValue, float.MaxValue, DataType.DataBlock, 2, indOffset+2){IsReadOnly = true},
            new Parameter<float>($"{boardName} - Значение эмуляции температуры", float.MinValue, float.MaxValue, DataType.DataBlock, 1, settOffset+2),
            new Parameter<bool>($"{boardName} - Эмуляция температуры", false, true, DataType.DataBlock, 1, settOffset,1),
            new Parameter<float>($"{boardName} - Максимальное значение температуры", float.MinValue, float.MaxValue, DataType.DataBlock, 1, settOffset+10)),
        new AnalogSensorInfo("Влажность", new Parameter<short>($"{boardName} - Значение АЦП влажности", 0, 32767, DataType.DataBlock, 2, indOffset+6){IsReadOnly = true},
            new Parameter<float>($"{boardName} - Влажность", float.MinValue, float.MaxValue, DataType.DataBlock, 2, indOffset+8){IsReadOnly = true},
            new Parameter<float>($"{boardName} - Значение эмуляции влажности", float.MinValue, float.MaxValue, DataType.DataBlock, 1, settOffset+6),
            new Parameter<bool>($"{boardName} - Эмуляция влажности", false, true, DataType.DataBlock, 1, settOffset,2),
            new Parameter<float>($"{boardName} - Максимальное значение влажности", float.MinValue, float.MaxValue, DataType.DataBlock, 1, settOffset+14)),
        
    ];
    
}