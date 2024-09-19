using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Measure;

public class MeasureSettings
{
    public Parameter<ushort> StanOpenTime { get; } =
        new Parameter<ushort>("Время стандартизации при открытом затворе, с.", 1, 20, DataType.DataBlock, 1, 1200);
    public Parameter<ushort> StanCloseTime { get; } =
        new Parameter<ushort>("Время стандартизации при закрытом затворе, с.", 1, 20, DataType.DataBlock, 1, 1202);
    public Parameter<DateTime> LastStandTime { get; } =
        new Parameter<DateTime>("Дата и время последней стандартизации", DateTime.MinValue, DateTime.MaxValue, DataType.DataBlock, 1, 1204);
    public Parameter<ushort> SingleMeasTime { get; } =
        new Parameter<ushort>("Время единичного измерения, с.", 1, 100, DataType.DataBlock, 1, 1220);
    
}