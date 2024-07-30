using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Measure;

public class MeasureIndicationAndControl
{
    #region Сброс ошибок

    public Parameter<bool> Rst { get; } =
        new Parameter<bool>("Сброс ошибок", false, true, DataType.DataBlock, 2, 1500, 0);

    #endregion
}