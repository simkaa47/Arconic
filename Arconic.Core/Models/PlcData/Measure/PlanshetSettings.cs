using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Measure;

public class PlanshetSettings
{
    public Parameter<float> K { get; } = new Parameter<float>("Доп. к-т",
        float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1262);
    
    public Parameter<float> ThickSv { get; } = new Parameter<float>("Толщина планшета, мкм",
        float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1266);
    public Parameter<float> Si { get; } =
        new Parameter<float>("Массовая доля Si, %", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1270);
    public Parameter<float> Fe { get; } =
        new Parameter<float>("Массовая доля Fe, %", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1274);
    public Parameter<float> Cu { get; } =
        new Parameter<float>("Массовая доля Cu, %", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1278);
    public Parameter<float> Mn { get; } =
        new Parameter<float>("Массовая доля Mn, %", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1282);
    public Parameter<float> Mg { get; } =
        new Parameter<float>("Массовая доля Mg, %", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1286);
    public Parameter<float> Cr { get; } =
        new Parameter<float>("Массовая доля Cr, %", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1290);
    public Parameter<float> Zn { get; } =
        new Parameter<float>("Массовая доля Zn, %", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1294);
    public Parameter<float> Ni { get; } =
        new Parameter<float>("Массовая доля Ni, %", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1298);
    public Parameter<float> Ti { get; } =
        new Parameter<float>("Массовая доля Ti, %", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1302);
    public Parameter<float> Be { get; } =
        new Parameter<float>("Массовая доля Be, %", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1306);
    public Parameter<float> Ga { get; } =
        new Parameter<float>("Массовая доля Ga, %", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1310);
    public Parameter<float> Pb { get; } =
        new Parameter<float>("Массовая доля Pb, %", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1314);
    public Parameter<float> Cd { get; } =
        new Parameter<float>("Массовая доля Cd, %", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1318);
    public Parameter<float> Zr { get; } =
        new Parameter<float>("Массовая доля Zr, %", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1322);
    public Parameter<float> Li { get; } =
        new Parameter<float>("Массовая доля Li, %", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1326);
    public Parameter<float> Na { get; } =
        new Parameter<float>("Массовая доля Na, %", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1330);
    public Parameter<float> Co { get; } =
        new Parameter<float>("Массовая доля Co, %", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1334);
    public Parameter<float> V { get; } =
        new Parameter<float>("Массовая доля V, %", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1338);
    public Parameter<float> As { get; } = 
        new Parameter<float>("Массовая доля As, %", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1342);
    public Parameter<float> Sc { get; } =
        new Parameter<float>("Массовая доля Sc, %", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1346);
    public Parameter<float> Ce { get; } =
        new Parameter<float>("Массовая доля Ce, %", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1350);
    
}