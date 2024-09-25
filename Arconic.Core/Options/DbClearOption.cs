namespace Arconic.Core.Options;

public class DbClearOption
{
    public const string SectionName = "SaveDataDaysPeriod";
    public int Measures { get; init; } 
    public int EventHistory { get; init; }
}