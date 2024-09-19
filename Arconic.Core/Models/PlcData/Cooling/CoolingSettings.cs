using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Cooling;

public class CoolingSettings
{
    public Parameter<bool> SbChiller { get; } =
        new Parameter<bool>("Питание чилера", false, true, DataType.DataBlock, 1, 1110, 1);
}