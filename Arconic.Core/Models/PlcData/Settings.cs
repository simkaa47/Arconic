using Arconic.Core.Models.PlcData.Cooling;
using Arconic.Core.Models.PlcData.Detectors;
using Arconic.Core.Models.PlcData.Drive;
using Arconic.Core.Models.PlcData.Measure;
using Arconic.Core.Models.PlcData.Safety;
using Arconic.Core.Models.PlcData.SingleMeasures;
using Arconic.Core.Models.PlcData.Source;
using Arconic.Core.Models.PlcData.SteelLabel;

namespace Arconic.Core.Models.PlcData;

public class Settings
{
    public SourceUnitSettings Source { get;} = new SourceUnitSettings();
    public MeasureSettings MeasureSettings { get; } = new MeasureSettings();
    public DetectorsSettings DetectorsSettings { get; } = new DetectorsSettings();
    public DriveSettings DriveSettings { get; } = new DriveSettings();
    public CoolingSettings CoolingSettings { get; } = new CoolingSettings();
    public SafetySettings SafetySettings { get; } = new SafetySettings();

    public SteelMagazineSettings SteelSettings { get; } = new SteelMagazineSettings();
    
    public List<SingleMeasuresList> SingleMeasures { get; } = Enumerable
        .Range(0,3)
        .Select(i=> new SingleMeasuresList(1400+i*288))
        .ToList();
}