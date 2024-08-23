using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Arconic.View.Converters;

public class GetByIndexFromParameterConverter:Converter
{
    public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null || parameter is null) return null;
        if (!int.TryParse(value.ToString(), out var index))
        {
            if(!TryCastToInt(value, out index)) return null;
        }
        if (!(parameter is IEnumerable<object> list)) return null;
        var enumerable = list as object[] ?? list.ToArray();
        if (enumerable.Count() < index + 1) return null;
        return enumerable.ElementAt(index);
    }
    
    private  bool TryCastToInt(object? obj, out int result)
    {
        if (obj is null)
        {
            result = 0;
            return false;
        }
        try
        {
            result = (int)obj;
            return true;
        }

        catch
        {
            result = 0;
            return false;
        }
    }
}