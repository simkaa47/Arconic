using System;
using System.Collections.Generic;
using System.Globalization;
using Arconic.Core.Models.AccessControl;
using Arconic.Core.Models.Event;
using Avalonia.Data.Converters;

namespace Arconic.View.Converters;

public class GetEventVisibilityConverter:IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count < 2) return 0.0;
        if (values[0] is null || values[0] is not EventHistoryItem item) return 0.0;
        if (item.EventType == EventType.HmiError)
        {
            if (values[1] is User user)
            {
                if(user.Level!= AccessLevel.Admin)
                    return 0.0;
            }
            else
            {
                return 0.0;
            }
        }

        return 1000.0;
    }
}