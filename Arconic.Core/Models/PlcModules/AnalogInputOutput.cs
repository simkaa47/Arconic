using Arconic.Core.Models.Parameters;

namespace Arconic.Core.Models.PlcModules;

public class AnalogInputOutput(string description, 
    Parameter<short> input,
    Parameter<bool> emulation,
    Parameter<short> emulationValue,
    Parameter<short> output)
{
    public string Position { get; init; } = string.Empty;
    public string Description { get; set;  } = description;
    public Parameter<short> Input { get; } = input;
    public Parameter<bool> Emulation { get; } = emulation;
    public Parameter<short> EmulationValue { get; } = emulationValue;
    public Parameter<short> Output { get; } = output;
}