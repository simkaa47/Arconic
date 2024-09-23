using Arconic.Core.Models.PlcData.Cooling;
using Arconic.Core.Models.PlcData.Detectors;
using Arconic.Core.Models.PlcData.Drive;
using Arconic.Core.Models.PlcData.Errors;
using Arconic.Core.Models.PlcData.Events;
using Arconic.Core.Models.PlcData.HighLevel;
using Arconic.Core.Models.PlcData.Measure;
using Arconic.Core.Models.PlcData.Safety;
using Arconic.Core.Models.PlcData.Source;
using Arconic.Core.Models.PlcData.SteelLabel;

namespace Arconic.Core.Models.PlcData;

public class ControlAndIndication
{
    public BsIndication BsIndication { get; } = new BsIndication();
    public MeasureIndicationAndControl MeasureIndicationAndControl { get; } = new MeasureIndicationAndControl();
    public HighLevelData HighLevelData { get; } = new HighLevelData();
    public DetectorsIndication DetectorsIndication { get; } = new DetectorsIndication();
    public PlcErrorsData PlcErrorsData { get; } = new PlcErrorsData();
    public PlcEventsData PlcEventsData { get; } = new PlcEventsData();
    public DriveIndication DriveIndication { get; } = new DriveIndication();
    public CoolingIndication CoolingIndication { get; } = new CoolingIndication();
    public SafetyIndication SafetyIndication { get; } = new SafetyIndication();
    public SteelMagazineIndication SteelMagazineIndication { get; } = new SteelMagazineIndication();
}

