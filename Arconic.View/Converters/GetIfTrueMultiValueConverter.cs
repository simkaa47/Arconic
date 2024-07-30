using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Arconic.View.Converters;

public class GetIfTrueMultiValueConverter:IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count < 3) return null;
        if (values[0] == null || values[0] is not bool condition) return null;
        return (condition ? values[2] : values[1]);
    }
}