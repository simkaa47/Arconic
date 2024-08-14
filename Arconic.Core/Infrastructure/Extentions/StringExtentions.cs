using System.Globalization;

namespace Arconic.Core.Infrastructure.Extentions;

public static class StringExtentions
{
    public static string ToTitleCase(this string source)
    {
        if (source.Length > 0)
        {
            source = source.ToLower();
            source = source.Insert(0, char.ToUpper(source[0]).ToString());
            source = source.Remove(1, 1);
        }

        return source;
    }
}