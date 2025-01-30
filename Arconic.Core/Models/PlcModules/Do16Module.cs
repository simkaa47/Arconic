using Arconic.Core.Models.Parameters;
using Arconic.Core.Models.PlcData;
using S7.Net;

namespace Arconic.Core.Models.PlcModules;

public class Do16Module:IPlcModule
{
    public string Description { get; } 
    public List<BoolSensorInfo> Sensors { get; }

    public Do16Module(string description, 
        int inputMemoryByteNum, 
        int emulatedMemoryByteNum, 
        int emulatedControlMemoryByteNum, 
        int outputMemoryByteNum, 
        DataType inputMemoryType = DataType.Memory,
        DataType emulatedMemoryType = DataType.Memory,
        DataType emulatedControlMemoryType = DataType.Memory,
        DataType outputMemoryType = DataType.Output, 
        DoModuleType doModuleType = DoModuleType.Do16)
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
                    $"{Description}, дискретный выход {i}, значение эмуляции",
                    false,
                    true,
                    emulatedMemoryType,
                    0,
                    emulatedMemoryByteNum + i / 8,
                    i % 8),
                new Parameter<bool>(
                    $"{Description}, дискретный выход {i}, эмуляция",
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
                    i % 8)){Position = GetPosition(i,doModuleType)}).ToList();
        
    }

    private static string GetPosition(int outNum, DoModuleType doModuleType)
    {
        switch (doModuleType)
        {
            case DoModuleType.Do16:
                return outNum<8 ? $"DQa{outNum}" : $"DQb{outNum%8}";
            case DoModuleType.Dio32:
                return $"DQ{16 + outNum}";
            default:
                return $"CH{outNum}";
        }
    }
}