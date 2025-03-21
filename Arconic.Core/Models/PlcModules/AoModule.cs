﻿using Arconic.Core.Models.Parameters;
using Arconic.Core.Models.PlcData;
using S7.Net;

namespace Arconic.Core.Models.PlcModules;

public class AoModule(
    string description,
    int inputMemoryByteNum,
    int emulatedMemoryByteNum,
    int emulatedControlMemoryByteNum,
    int outputMemoryByteNum,
    DataType inputMemoryType = DataType.Memory,
    DataType emulatedMemoryType = DataType.Memory,
    DataType emulatedControlMemoryType = DataType.Memory,
    DataType outputMemoryType = DataType.Output,
    int sensorsNum = 4)
    : AioModule(description, inputMemoryByteNum, emulatedMemoryByteNum,
        emulatedControlMemoryByteNum, outputMemoryByteNum, inputMemoryType,
        emulatedMemoryType, emulatedControlMemoryType, outputMemoryType, sensorsNum)
{
    protected override string GetInputName() => "выход";
    protected override string GetPositionName() => "AQ";
}