using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Safety;

public class SafetyIndication
{
        public Parameter<bool> SbStopHmi { get; } =
        new Parameter<bool>("Кнопка STOP на HMI", false, true, DataType.DataBlock, 2, 700, 5);
}