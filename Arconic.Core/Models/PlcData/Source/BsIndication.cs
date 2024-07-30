using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Source;

public class BsIndication()
{
    public Parameter<ushort> Voltage { get; } = new Parameter<ushort>("Текущее значение напряжения генератора, кВ", 0,
        ushort.MaxValue, DataType.DataBlock, 2, 0, 0){ IsReadOnly = true };
    
    public Parameter<ushort> Current { get; } = new Parameter<ushort>("Текущее значение тока генератора, mA", 0,
        ushort.MaxValue, DataType.DataBlock, 2, 2, 0){ IsReadOnly = true };

    public Parameter<float> WorkHours { get; } = new Parameter<float>("Моточасы генератора",
        float.MinValue,
        float.MaxValue, DataType.DataBlock, 2, 4, 0) { IsReadOnly = true };
    
    public Parameter<ushort> UnitTemp { get; } = new Parameter<ushort>("Температура блока управления, С", ushort.MinValue,
        ushort.MaxValue, DataType.DataBlock, 2, 8, 0){ IsReadOnly = true };
    
    public Parameter<ushort> HeatLeftSec { get; } = new Parameter<ushort>("Осталось до конца кондиционирования, с.", ushort.MinValue,
        ushort.MaxValue, DataType.DataBlock, 2, 10, 0){ IsReadOnly = true };
    
    public Parameter<bool> NoErr { get; } = new Parameter<bool>("Нет ошибок", false,
        true, DataType.DataBlock, 2, 12, 0){ IsReadOnly = true };
    
    public Parameter<bool> HvOn { get; } = new Parameter<bool>("HV включено", false,
        true, DataType.DataBlock, 2, 12, 1){ IsReadOnly = true };
    
    public Parameter<bool> CommOk { get; } = new Parameter<bool>("Связь с генератором есть", false,
        true, DataType.DataBlock, 2, 12, 2){ IsReadOnly = true };
    
    public Parameter<bool> PowerOn { get; } = new Parameter<bool>("Питанине на генератор есть", false,
        true, DataType.DataBlock, 2, 12, 3){ IsReadOnly = true };
    
    public Parameter<bool> MeasReady { get; } = new Parameter<bool>("Готовность к измерениям", false,
        true, DataType.DataBlock, 2, 12, 4){ IsReadOnly = true };
}