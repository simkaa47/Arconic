using System;
using System.Globalization;
using Arconic.Core.Models.AccessControl;

namespace Arconic.View.Converters;

public class UserAccessConverter:Converter
{
    public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null || value is not User user || parameter is not AccessLevel accessLevel)
        {
            return false;
        }

        return user.Level>=accessLevel;
    }
}