using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Safety;

public class SafetySettings
{
    public Parameter<bool> SqHatchTubeEmulOnOff { get; } =
        new Parameter<bool>("Трубка генератора - эмуляция", false, true, DataType.DataBlock, 1, 1100, 0);
    public Parameter<bool> SqGeneratorEmulOnOff1 { get; } =
        new Parameter<bool>("Генератор 1 - эмуляция", false, true, DataType.DataBlock, 1, 1100, 1);
    public Parameter<bool> SqGeneratorEmulOnOff2 { get; } =
        new Parameter<bool>("Генератор 2 - эмуляция", false, true, DataType.DataBlock, 1, 1100, 2);
    public Parameter<bool> SqRackEmulOnOff { get; } =
        new Parameter<bool>("Датчик рамы - эмуляция", false, true, DataType.DataBlock, 1, 1100, 3);
    public Parameter<bool> SqDetectorsEmulOnOff { get; } =
        new Parameter<bool>("Блок детектора - эмуляция", false, true, DataType.DataBlock, 1, 1100, 4);
    
    public Parameter<bool> SqHatchTubeEmulValue { get; } =
        new Parameter<bool>("Трубка генератора - значение эмуляции", false, true, DataType.DataBlock, 1, 1101, 0);
    public Parameter<bool> SqGeneratorEmulValue1 { get; } =
        new Parameter<bool>("Генератор 1 - значение эмуляции", false, true, DataType.DataBlock, 1, 1101, 1);
    public Parameter<bool> SqGeneratorEmulValue2 { get; } =
        new Parameter<bool>("Генератор 2 - значение эмуляции", false, true, DataType.DataBlock, 1, 1101, 2);
    public Parameter<bool> SqRackEmulValue { get; } =
        new Parameter<bool>("Датчик рамы - значение эмуляции", false, true, DataType.DataBlock, 1, 1101, 3);
    public Parameter<bool> SqDetectorsEmulValue { get; } =
        new Parameter<bool>("Блок детектора - значение эмуляции", false, true, DataType.DataBlock, 1, 1101, 4);
}