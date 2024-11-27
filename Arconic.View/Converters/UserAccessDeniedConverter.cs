using System;
using System.Collections.Generic;
using System.Globalization;
using Arconic.Core.Models.AccessControl;
using Avalonia.Data.Converters;

namespace Arconic.View.Converters;

public class UserAccessDeniedConverter:IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count < 2) return true;
        if (values[0] is null || values[0] is not User user) return true;
        if (values[1] is null || values[1] is not AccessLevel level) return true;
        return user.Level < level;
    }
}