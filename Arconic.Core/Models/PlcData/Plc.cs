using Arconic.Core.Models.Parameters;
using Arconic.Core.Models.PlcModules;

namespace Arconic.Core.Models.PlcData;

public class Plc
{
    public Settings Settings { get; } = new Settings();
    public ControlAndIndication ControlAndIndication { get; } = new ControlAndIndication();
    public PlcConfig Config { get; } = new PlcConfig();

}