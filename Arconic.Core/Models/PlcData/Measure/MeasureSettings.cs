using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Measure;

public class MeasureSettings
{
    public Parameter<ushort> StanOpenTime { get; } =
        new Parameter<ushort>("Время стандартизации при открытом затворе, мс.", 1000, 20000, DataType.DataBlock, 1, 1200);
    public Parameter<ushort> StanCloseTime { get; } =
        new Parameter<ushort>("Время стандартизации при закрытом затворе, мс.", 1000, 20000, DataType.DataBlock, 1, 1202);
    public Parameter<DateTime> LastStandTime { get; } =
        new Parameter<DateTime>("Дата и время последней стандартизации", DateTime.MinValue, DateTime.MaxValue, DataType.DataBlock, 1, 1204);
    public Parameter<ushort> SingleMeasTime { get; } =
        new Parameter<ushort>("Время единичного измерения, с.", 1, 100, DataType.DataBlock, 1, 1220);
    
    public Parameter<ushort> TemperatureSv { get; } =
        new Parameter<ushort>("Уставка температуры, С.", 25, 1600, DataType.DataBlock, 1, 1228);
    
    public Parameter<ushort> CloseGateTimeSv { get; } = new Parameter<ushort>("Время закрытия затвора после полосы, с",
        0, 600, DataType.DataBlock, ParameterBase.SettingsDbNum, 1222);
    public Parameter<ushort> AverageTime { get; } = new Parameter<ushort>("Время усреднения (режим ЦЛ), мс",
        100, 1000, DataType.DataBlock, ParameterBase.SettingsDbNum, 1224);
    public Parameter<ushort> SendDataToAsutpPeriod { get; } = new Parameter<ushort>("Период выдачи данных толщины в АСУ (режим ЦЛ), мс",
        100, 1000, DataType.DataBlock, ParameterBase.SettingsDbNum, 1226);
    
}