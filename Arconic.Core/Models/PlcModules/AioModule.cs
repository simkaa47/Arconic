using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcModules;

public abstract class AioModule:IPlcModule
{
    public string Description { get; }
    public List<AnalogInputOutput> Sensors { get; }

    protected AioModule(string description, 
        int inputMemoryByteNum, 
        int emulatedMemoryByteNum, 
        int emulatedControlMemoryByteNum, 
        int outputMemoryByteNum, 
        DataType inputMemoryType,
        DataType emulatedMemoryType,
        DataType emulatedControlMemoryType,
        DataType outputMemoryType,
        int sensorsNum)
    {
        Description = description;
        Sensors = Enumerable.Range(0, sensorsNum)
            .Select(i => new AnalogInputOutput("Резерв",
                new Parameter<short>("",
                    short.MinValue,
                    short.MaxValue,
                    inputMemoryType,
                    0,
                    inputMemoryByteNum + i * 2),
                new Parameter<bool>($"{Description}, аналоговый {GetInputName()} {i}, эмуляция",
                    false,
                    true,
                    emulatedControlMemoryType,
                    0,
                    emulatedControlMemoryByteNum + i / 8,
                    i % 8),
                new Parameter<short>($"{Description}, аналоговый {GetInputName()} {i}, значение эмуляции",
                    short.MinValue,
                    short.MaxValue,
                    emulatedMemoryType,
                    0,
                    emulatedMemoryByteNum + i * 2),
                new Parameter<short>("",
                    short.MinValue,
                    short.MaxValue,
                    outputMemoryType,
                    0,
                    outputMemoryByteNum + i*2)){Position = $"{GetPositionName()}{i}"}).ToList();
    }

    protected virtual string GetInputName() => "вход/выход";
    protected virtual string GetPositionName() => "CH";
    
}