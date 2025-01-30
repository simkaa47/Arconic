using Arconic.Core.Models.Parameters;
using Arconic.Core.Models.PlcData;
using S7.Net;

namespace Arconic.Core.Models.PlcModules;

public class AoModule:IPlcModule
{
    public string Description { get; }
    public List<AnalogInputOutput> Sensors { get; }

    public AoModule(string description, 
        int inputMemoryByteNum, 
        int emulatedMemoryByteNum, 
        int emulatedControlMemoryByteNum, 
        int outputMemoryByteNum, 
        DataType inputMemoryType = DataType.Memory,
        DataType emulatedMemoryType = DataType.Memory,
        DataType emulatedControlMemoryType = DataType.Memory,
        DataType outputMemoryType = DataType.Output,
        int sensorsNum = 4)
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
                new Parameter<bool>($"{Description}, аналоговый выход {i}, эмуляция",
                    false,
                    true,
                    emulatedControlMemoryType,
                    0,
                    emulatedControlMemoryByteNum + i / 8,
                    i % 8),
                new Parameter<short>($"{Description}, аналоговый выход {i}, значение эмуляции",
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
                    outputMemoryByteNum + i*2)){Position = $"AQ{i}"}).ToList();
    }
    
}