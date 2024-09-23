using Arconic.Core.Models.Parameters;

namespace Arconic.Core.Models.PlcData;

public record AnalogSensorInfo(string Description, Parameter<short> AdcValue, 
    Parameter<float> RealValue,
    Parameter<float> EmulValue, 
    Parameter<bool> EmulOnOff,
    Parameter<float> MaxValue);