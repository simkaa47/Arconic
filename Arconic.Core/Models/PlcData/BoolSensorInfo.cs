using Arconic.Core.Models.Parameters;

namespace Arconic.Core.Models.PlcData;

public record BoolSensorInfo(
    string Description,
    Parameter<bool> RealValue,
    Parameter<bool> EmulValue,
    Parameter<bool> EmulOnOff,
    Parameter<bool> Result);
