using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.HighLevel;

public class HighLevelData
{
    #region Связь с АСУ

    public Parameter<bool> CommState { get; } =
        new Parameter<bool>("Связь с АСУ", false, true, DataType.DataBlock, 2, 1438, 0);

    #endregion

    public List<StripInfo> Coils { get; } =
    [
        new StripInfo(820, "Предыдущая полоса"),
        new StripInfo(1026, "Текущая полоса"),
        new StripInfo(1232, "Следующая полоса", true)
    ];
    
}