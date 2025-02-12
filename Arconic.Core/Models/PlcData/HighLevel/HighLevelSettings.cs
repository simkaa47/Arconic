using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.HighLevel;

public class HighLevelSettings
{
    public List<Parameter<Byte>> IbaIp { get;  } = Enumerable.Range(0,4).Select(i=> new Parameter<Byte>(
            $"IP адрес сервера IBA, ,байт {i}",
            0,
            255,
            DataType.DataBlock,
            ParameterBase.SettingsDbNum,
            1396+i))
        .ToList();

    public Parameter<ushort> IbaInterface { get; } = new Parameter<ushort>("Тип интерфеса сервера IBA",
        0,
        1,
        DataType.DataBlock,
        ParameterBase.SettingsDbNum,
        1394);
}