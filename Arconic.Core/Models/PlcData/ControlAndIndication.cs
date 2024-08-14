﻿using Arconic.Core.Models.PlcData.Detectors;
using Arconic.Core.Models.PlcData.Errors;
using Arconic.Core.Models.PlcData.Events;
using Arconic.Core.Models.PlcData.HighLevel;
using Arconic.Core.Models.PlcData.Measure;
using Arconic.Core.Models.PlcData.Source;

namespace Arconic.Core.Models.PlcData;

public class ControlAndIndication
{
    public BsIndication BsIndication { get; } = new BsIndication();
    public MeasureIndicationAndControl MeasureIndicationAndControl { get; } = new MeasureIndicationAndControl();
    public HighLevelData HighLevelData { get; } = new HighLevelData();
    public DetectorsIndication DetectorsIndication { get; } = new DetectorsIndication();
    public PlcErrorsData PlcErrorsData { get; } = new PlcErrorsData();
    public PlcEventsData PlcEventsData { get; } = new PlcEventsData();
}

