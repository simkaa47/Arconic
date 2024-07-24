using System.ComponentModel.DataAnnotations;

namespace Arconic.Core.Infrastructure.Attributes;

public sealed class InRangeAttribute(string propA, string propB) : ValidationAttribute
{
    private string PropA { get; } = propA;
    private string PropB { get; } = propB;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        object instance = validationContext.ObjectInstance;
        var a = instance.GetType()?.GetProperty(PropA)?.GetValue(instance);
        var b = instance.GetType()?.GetProperty(PropB)?.GetValue(instance);
        if (a is not null && b is not null && value is not null)
        {
            if (!(value is IComparable valueComp)) return new($"Value is not IComparable");
            if (valueComp.CompareTo(a) >= 0 && valueComp.CompareTo(b) <= 0)
            {
                return ValidationResult.Success;
            }
            else
                return new($"Value must be between {a} and {b}");

        }
        else
        {
            return new($"Enable to get instances {PropA} and {PropB}");
        }
    }
}