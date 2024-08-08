﻿using Arconic.Core.Models.PlcData.Detectors;
using Arconic.Core.Models.PlcData.Source;

namespace Arconic.Core.Models.PlcData;

public class Settings
{
    public SourceUnitSettings Source { get;} = new SourceUnitSettings();
    public DetectorsSettings DetectorsSettings { get; } = new DetectorsSettings();
}