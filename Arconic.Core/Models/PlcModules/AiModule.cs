using S7.Net;

namespace Arconic.Core.Models.PlcModules;

public class AiModule(
    string description,
    int inputMemoryByteNum,
    int emulatedMemoryByteNum,
    int emulatedControlMemoryByteNum,
    int outputMemoryByteNum,
    DataType inputMemoryType = DataType.Input,
    DataType emulatedMemoryType = DataType.Memory,
    DataType emulatedControlMemoryType = DataType.Memory,
    DataType outputMemoryType = DataType.Memory,
    int sensorsNum = 4)
    : AioModule(description, inputMemoryByteNum, emulatedMemoryByteNum,
        emulatedControlMemoryByteNum, outputMemoryByteNum, inputMemoryType,
        emulatedMemoryType, emulatedControlMemoryType, outputMemoryType, sensorsNum)
{
    protected override string GetInputName() => "вход";
    protected override string GetPositionName() => "AI";
}