using Arconic.Core.Infrastructure.DataContext;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Arconic.Core.Models.Trends;

public partial class Scan:Entity
{
    public List<ThickPoint> ThickPoints { get;  set; } = new List<ThickPoint>();
    public float Width { get; set; }
    public float Klin { get; set; } 
    public float Chechewitsa { get; set; }
    public long StripId { get; set; }
    public Strip? Strip { get; init; }
    public float CentralLineDeviation { get; set; }
    public int ScanNumber { get; set; }

    public void OrderPoints()
    {
        ThickPoints = ThickPoints.OrderBy(p => p.Position).ToList();
    }
}