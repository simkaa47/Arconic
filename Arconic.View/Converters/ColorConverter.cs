using System;
using System.Globalization;
using Avalonia.Media;

namespace Arconic.View.Converters;

public class ColorConverter:Converter
{
    public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var color = Colors.Black;
        Color.TryParse(value?.ToString() ?? "#000000", out color);
        return color;
    }

    public override object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }
}