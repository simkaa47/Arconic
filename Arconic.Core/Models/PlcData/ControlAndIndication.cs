using Arconic.Core.Models.PlcData.Measure;
using Arconic.Core.Models.PlcData.Source;

namespace Arconic.Core.Models.PlcData;

public class ControlAndIndication
{
    public BsIndication BsIndication { get; } = new BsIndication();
    public MeasureIndicationAndControl MeasureIndicationAndControl { get; } = new MeasureIndicationAndControl();
}

