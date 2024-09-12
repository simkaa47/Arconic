﻿using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Source;

public class BsIndication()
{
    public Parameter<float> Voltage { get; } = new Parameter<float>("Текущее значение напряжения генератора, кВ", float.MinValue,
        float.MaxValue, DataType.DataBlock, 2, 0){ IsReadOnly = true };
    
    public Parameter<float> Current { get; } = new Parameter<float>("Текущее значение тока генератора, mA", float.MinValue,
        float.MaxValue, DataType.DataBlock, 2, 4){ IsReadOnly = true };

    public Parameter<float> WorkHours { get; } = new Parameter<float>("Моточасы генератора",
        float.MinValue,
        float.MaxValue, DataType.DataBlock, 2, 8) { IsReadOnly = true };
    
    public Parameter<float> UnitTemp { get; } = new Parameter<float>("Температура блока управления, С", ushort.MinValue,
        ushort.MaxValue, DataType.DataBlock, 2, 12){ IsReadOnly = true };
    
    public Parameter<ushort> HeatLeftSec { get; } = new Parameter<ushort>("Осталось до конца кондиционирования, с.", ushort.MinValue,
        ushort.MaxValue, DataType.DataBlock, 2, 16){ IsReadOnly = true };
    
    public Parameter<bool> NoErr { get; } = new Parameter<bool>("Нет ошибок", false,
        true, DataType.DataBlock, 2, 18, 0){ IsReadOnly = true };
    
    public Parameter<bool> HvOn { get; } = new Parameter<bool>("HV включено", false,
        true, DataType.DataBlock, 2, 18, 1){ IsReadOnly = true };
    
    public Parameter<bool> CommOk { get; } = new Parameter<bool>("Связь с генератором есть", false,
        true, DataType.DataBlock, 2, 18, 2){ IsReadOnly = true };
    
    public Parameter<bool> PowerOn { get; } = new Parameter<bool>("Питанине на генератор есть", false,
        true, DataType.DataBlock, 2, 18, 3){ IsReadOnly = true };
    
    public Parameter<bool> MeasReady { get; } = new Parameter<bool>("Готовность к измерениям", false,
        true, DataType.DataBlock, 2, 18, 4){ IsReadOnly = true };
}