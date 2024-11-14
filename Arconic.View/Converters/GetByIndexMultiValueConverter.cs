using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;

namespace Arconic.View.Converters;

public class GetByIndexMultiValueConverter:IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        int index = 0;
        if (values.Count<2)return null;
        if (values[0] is null) return null;
        if (!(values[1] is IEnumerable<object> list)) return null;
        if (!int.TryParse(values[0]!.ToString(), out index)) return null;
        if (list.Count() < index + 1) return null;
        return list.ElementAt(index);
    }
}