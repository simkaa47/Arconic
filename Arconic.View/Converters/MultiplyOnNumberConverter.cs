using System;
using System.Globalization;

namespace Arconic.View.Converters;

public class MultiplyOnNumberConverter:Converter
{
    public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null || parameter is null) return value;
        if (!float.TryParse(value.ToString(), out float input)) return value;
        if (!float.TryParse(parameter.ToString(), out float k)) return value;
        return k * input;
    }
}