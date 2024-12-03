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
}