using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.HighLevel;

public class StripInfo(int offset, string name, bool isEdited = false)
{
    public string Name { get; } = name;
    private bool IsEdited { get; } = isEdited;

    public Parameter<string> SteelLabel { get; } =  new Parameter<string>("Марка стали", "", "zzzzzzzzzzzzzzzzzzzzzzzzzzz",
        DataType.DataBlock, 2, offset, 0){IsReadOnly = !isEdited, Length = 26};
    public Parameter<string> StripId { get; } = new Parameter<string>("ID полосы", "", "zzzzzzzzzz",
        DataType.DataBlock, 2, offset+26, 0){IsReadOnly = !isEdited, Length = 12};
    public Parameter<string> Date { get; } = new Parameter<string>("Дата", "", "zzzzzzzzzzzzzzz",
        DataType.DataBlock, 2, offset+174, 0){IsReadOnly = !isEdited, Length = 8};
    public Parameter<string> Time { get; } = new Parameter<string>("Время", "", "zzzzzzzzzzzzzzz",
        DataType.DataBlock, 2, offset+166, 0){IsReadOnly = !isEdited, Length = 8};
    public Parameter<ushort> Mode { get; } = new Parameter<ushort>("Режим толщиномера", 0, 2,
        DataType.DataBlock, 2, offset+184, 0){IsReadOnly = !isEdited};
    public Parameter<float> ExpectedThick { get; } = new Parameter<float>("Ожидаемая толщина", 0, float.MaxValue,
        DataType.DataBlock, 2, offset+186, 0){IsReadOnly = !isEdited};
    public Parameter<float> ExpectedWidth{ get; } = new Parameter<float>("Ожидаемая ширина, мм", 0, float.MaxValue,
        DataType.DataBlock, 2, offset+190, 0){IsReadOnly = !isEdited};
    public Parameter<float> AddCoeff { get; } = new Parameter<float>("Дполнительный мультипликативный к-т", 0, float.MaxValue,
        DataType.DataBlock, 2, offset+202, 0){IsReadOnly = !isEdited};
    
    public Parameter<float> Si { get; } =
        new Parameter<float>("Массовая доля Si, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, offset + 38);
    public Parameter<float> Fe { get; } =
        new Parameter<float>("Массовая доля Fe, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, offset + 42);
    public Parameter<float> Cu { get; } =
        new Parameter<float>("Массовая доля Cu, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, offset + 46);
    public Parameter<float> Mn { get; } =
        new Parameter<float>("Массовая доля Mn, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, offset + 50);
    public Parameter<float> Mg { get; } =
        new Parameter<float>("Массовая доля Mg, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, offset + 54);
    public Parameter<float> Cr { get; } =
        new Parameter<float>("Массовая доля Cr, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, offset + 58);
    public Parameter<float> Zn { get; } =
        new Parameter<float>("Массовая доля Zn, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, offset + 62);
    public Parameter<float> Ni { get; } =
        new Parameter<float>("Массовая доля Ni, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, offset + 66);
    public Parameter<float> Ti { get; } =
        new Parameter<float>("Массовая доля Ti, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, offset + 70);
    public Parameter<float> Be { get; } =
        new Parameter<float>("Массовая доля Be, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, offset + 74);
    public Parameter<float> Ga { get; } =
        new Parameter<float>("Массовая доля Ga, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, offset + 78);
    public Parameter<float> Pb { get; } =
        new Parameter<float>("Массовая доля Pb, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, offset + 82);
    public Parameter<float> Cd { get; } =
        new Parameter<float>("Массовая доля Cd, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, offset + 86);
    public Parameter<float> Zr { get; } =
        new Parameter<float>("Массовая доля Zr, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, offset + 90);
    public Parameter<float> Li { get; } =
        new Parameter<float>("Массовая доля Li, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, offset + 94);
    public Parameter<float> Na { get; } =
        new Parameter<float>("Массовая доля Na, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, offset + 98);
    public Parameter<float> Co { get; } =
        new Parameter<float>("Массовая доля Co, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, offset + 102);
    public Parameter<float> V { get; } =
        new Parameter<float>("Массовая доля V, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, offset + 106);
    public Parameter<float> As { get; } =
        new Parameter<float>("Массовая доля As, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, offset + 110);
    public Parameter<float> Sc { get; } =
        new Parameter<float>("Массовая доля Sc, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, offset + 114);
    public Parameter<float> Ce { get; } =
        new Parameter<float>("Массовая доля Ce, %", float.MinValue, float.MaxValue, DataType.DataBlock, 2, offset + 118);
}