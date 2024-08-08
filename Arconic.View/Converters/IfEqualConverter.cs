using System;
using System.Globalization;

namespace Arconic.View.Converters;

public class IfEqualConverter : Converter
{
    public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (parameter is null) return false;
        if (value is null) return false;
        return value.ToString() == parameter.ToString();
    }
}