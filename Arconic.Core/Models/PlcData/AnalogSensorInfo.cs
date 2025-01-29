using Arconic.Core.Models.Parameters;

namespace Arconic.Core.Models.PlcData;

public class AnalogSensorInfo(
    string description,
    Parameter<short> adcValue,
    Parameter<float> realValue,
    Parameter<float> emulValue,
    Parameter<bool> emulOnOff,
    Parameter<float> maxValue) : ISensorInfo
{
    public string Description { get; set;  } = description;
    public Parameter<short> AdcValue { get; } = adcValue;
    public Parameter<float> RealValue { get; } = realValue;
    public Parameter<float> EmulValue { get; } = emulValue;
    public Parameter<bool> EmulOnOff { get; } = emulOnOff;
    public Parameter<float> MaxValue { get; } = maxValue;
}