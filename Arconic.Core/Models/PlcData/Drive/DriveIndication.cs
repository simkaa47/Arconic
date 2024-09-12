using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Drive;

public class DriveIndication
{
    public Parameter<short> DriveStatus { get; } =
        new Parameter<short>("Статус перемещения рамы", 0, 20, DataType.DataBlock, 2, 224);
}