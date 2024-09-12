using Arconic.Core.Infrastructure.DataContext;

namespace Arconic.Core.Models.Trends;

public class ThickPoint : Entity
{
    public float Thick { get; init; } 
    public float Lendth { get; init; } 
    public float Position { get; init;} 
    public DateTime DateTime { get; init;} 
    public Scan? Scan { get; set; }
    public long? ScanId { get; set; }
    public Strip? Strip { get; set; }
    public long? StripId { get; set; }
    public float Speed { get; set; }
}