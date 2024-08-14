using System;
using System.Globalization;
using Arconic.Core.Models.Event;
using Avalonia.Media;

namespace Arconic.View.Converters;

public class GetEventColor:Converter
{
    public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null || value is not EventHistoryItem item) return Colors.Transparent;
        switch (item.EventType)
        {
            case EventType.PlcEvent:
                return new SolidColorBrush(Colors.Yellow, 0.66);
            case EventType.PlcError:
                if (item.IsActive)
                {
                    return new SolidColorBrush(Colors.Red, 0.9);
                }
                return new SolidColorBrush(Colors.LimeGreen, 0.9);
            case EventType.HmiError:
                return new SolidColorBrush(Colors.Orange, 1);
            default:
                return new SolidColorBrush(Colors.Transparent);
        }
    }
}