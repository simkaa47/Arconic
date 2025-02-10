using System.Collections.Concurrent;
using Arconic.Core.Infrastructure.DataContext;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Arconic.Core.Models.Trends;

public partial class Scan:Entity
{
    private object _lock = new();
    public List<ThickPoint> ThickPoints { get; private set; } = new List<ThickPoint>();
    public float Width { get; set; }
    public float Klin { get; set; } 
    public float Chechewitsa { get; set; }
    public long StripId { get; set; }
    public Strip? Strip { get; init; }
    public float CentralLineDeviation { get; set; }
    public int ScanNumber { get; set; }

    public void SetThickPoints(List<ThickPoint> points)
    {
        lock (_lock)
        {
            ThickPoints = points;
        }
    }
}