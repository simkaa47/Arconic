using Arconic.Core.Models.Parameters;

namespace Arconic.Core.Models.PlcData;

public class BoolSensorInfo(
    string description,
    Parameter<bool> realValue,
    Parameter<bool> emulValue,
    Parameter<bool> emulOnOff,
    Parameter<bool> result) : ISensorInfo
{
    public string Position { get; init; } = string.Empty;
    public string Description { get; set;  } = description;
    public Parameter<bool> RealValue { get; } = realValue;
    public Parameter<bool> EmulValue { get; } = emulValue;
    public Parameter<bool> EmulOnOff { get; } = emulOnOff;
    public Parameter<bool> Result { get; } = result;
}
