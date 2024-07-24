namespace Arconic.Core.Models.PlcData;

public class Plc
{
    public DiscreteOutputs Do { get; } = new DiscreteOutputs();
    public DiscreteInputs Di { get; } = new DiscreteInputs();
    public Settings Settings { get; } = new Settings();
    public Indication Indication { get; } = new Indication();
}