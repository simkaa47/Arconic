using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Safety;

public class SafetyIndication
{
    public Parameter<bool> SqHatchTubeResult { get; } =
        new Parameter<bool>("Трубка генератора", false, true, DataType.DataBlock, 2, 700, 0);
    public Parameter<bool> SqGeneratorResult1 { get; } =
        new Parameter<bool>("Генератор 1", false, true, DataType.DataBlock, 2, 700, 1);
    public Parameter<bool> SqGeneratorResult2 { get; } =
        new Parameter<bool>("Генератор 2", false, true, DataType.DataBlock, 2, 700, 2);
    public Parameter<bool> SqRackResult { get; } =
        new Parameter<bool>("Датчик рамы", false, true, DataType.DataBlock, 2, 700, 3);
    public Parameter<bool> SqDetectorsResult { get; } =
        new Parameter<bool>("Блок детектора", false, true, DataType.DataBlock, 2, 700, 4);
    public Parameter<bool> SbConsoleAbort { get; } =
        new Parameter<bool>("Кнопка аварийного стопа (ЛПУ)", false, true, DataType.Input, 0, 10, 2);
    public Parameter<bool> SbAbort { get; } =
        new Parameter<bool>("Кнопка аварийного стопа (ШЭ)", false, true, DataType.Input, 0, 1, 2);
    public Parameter<bool> HlAbort { get; } =
        new Parameter<bool>("Лампа \"Стоп\" на ЛПУ", false, true, DataType.Output, 0, 11, 2);
    
    
    
    public Parameter<bool> SqHatchTube { get; } =
        new Parameter<bool>("Трубка генератора", false, true, DataType.Input, 0, 14, 0);
    public Parameter<bool> SqGenerator1 { get; } =
        new Parameter<bool>("Генератор 1", false, true, DataType.Input, 0, 14, 1);
    public Parameter<bool> SqGenerator2 { get; } =
        new Parameter<bool>("Генератор 2", false, true, DataType.Input, 0, 14, 2);
    public Parameter<bool> SqRack { get; } =
        new Parameter<bool>("Датчик рамы", false, true, DataType.Input, 2, 14, 3);
    public Parameter<bool> SqDetectors { get; } =
        new Parameter<bool>("Блок детектора", false, true, DataType.Input, 2, 14, 4);
    
    public Parameter<bool> SbStopHmi { get; } =
        new Parameter<bool>("Кнопка STOP на HMI", false, true, DataType.DataBlock, 2, 700, 5);
}