using System;
using System.Globalization;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Arconic.View.Converters;

public class StringToBitmapConverter:Converter
{
    public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null || value is not string path) return null;
        var str = $"avares:/{path}";
        var uri = new Uri(str);
        if (!AssetLoader.Exists(uri)) return null;
        return new Bitmap(path);
    }
}