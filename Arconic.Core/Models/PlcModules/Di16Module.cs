using Arconic.Core.Models.Parameters;
using Arconic.Core.Models.PlcData;
using S7.Net;

namespace Arconic.Core.Models.PlcModules;

public class Di16Module:IPlcModule
{
    public string Description { get; } 
    public List<BoolSensorInfo> Sensors { get; }

    public Di16Module(string description, 
        int inputMemoryByteNum, 
        int emulatedMemoryByteNum, 
        int emulatedControlMemoryByteNum, 
        int outputMemoryByteNum, 
        DataType inputMemoryType = DataType.Input,
        DataType emulatedMemoryType = DataType.Memory,
        DataType emulatedControlMemoryType = DataType.Memory,
        DataType outputMemoryType = DataType.Memory,
        int initaialSchemePosition = 0)
    {
        Description = description;
        Sensors = Enumerable.Range(0, 16)
            .Select(i => new BoolSensorInfo("Резерв",
                new Parameter<bool>("",
                    false,
                    true,
                    inputMemoryType,
                    0,
                    inputMemoryByteNum + i / 8,
                    i % 8),
                new Parameter<bool>(
                    $"{Description}, дискретный вход {i}, значение эмуляции",
                    false,
                    true,
                    emulatedMemoryType,
                    0,
                    emulatedMemoryByteNum + i / 8,
                    i % 8),
                new Parameter<bool>(
                    $"{Description}, дискретный вход {i}, эмуляция",
                    false,
                    true,
                    emulatedControlMemoryType,
                    0,
                    emulatedControlMemoryByteNum + i / 8,
                    i % 8),
                new Parameter<bool>("",
                    false,
                    true,
                    outputMemoryType,
                    0,
                    outputMemoryByteNum + i / 8,
                    i % 8)){Position = $"CH{i+initaialSchemePosition}"}).ToList();
       
    }
    
    
}