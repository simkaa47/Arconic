using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Arconic.Core.Infrastructure.DataContext;
using Arconic.Core.Models.PlcData.Drive;

namespace Arconic.Core.Models.Trends;

public class Strip:Entity
{
    public List<Scan> Scans { get; set; } = new List<Scan>();
    [MaxLength(16)]
    public string StripId { get; init; } = string.Empty;
    [MaxLength(26)]
    public string SteelLabel { get; init; } = string.Empty;
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; set; }
    public float ExpectedThick { get; init; }
    public float MinExpectedThick { get; init; }
    public float MaxExpectedThick { get; init; }
    public float ExpectedWidth { get; init; }
    public float MinExpectedWidth { get; init; }
    public float MaxExpectedWidth { get; init; }
    public float CentralLinePosition { get; init; }

    public MeasModes MeasMode { get; init; }
    public List<ThickPoint> ThickPoints { get; } = new List<ThickPoint>();
    [NotMapped]
    public float Length { get; set; }
    [NotMapped]
    public float AverageThick { get; set; }
    [NotMapped]
    public float MinThick { get; set; }
    [NotMapped]
    public float MaxThick { get; set; }
    [NotMapped]
    public float AverageWidth { get; set; }
    [NotMapped]
    public float MinWidth { get; set; }
    [NotMapped]
    public float AverageCentralLineDeviation { get; set; }
    [NotMapped]
    public float MaxWidth { get; set; }
    [NotMapped]
    public float AverageKlin { get; set; }
    [NotMapped]
    public float MinKlin { get; set; }
    [NotMapped]
    public float MaxKlin { get; set; }
    [NotMapped]
    public float AverageChehevitsa { get; set; }
    [NotMapped]
    public float MinChehevitsa { get; set; }
    [NotMapped]
    public float MaxChehevitsa { get; set; }
    
}