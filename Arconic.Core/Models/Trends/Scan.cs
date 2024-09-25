using Arconic.Core.Infrastructure.DataContext;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Arconic.Core.Models.Trends;

public partial class Scan:Entity
{
    public List<ThickPoint> ThickPoints { get; private set; } = new List<ThickPoint>();
    [ObservableProperty]
    private float _width;
    [ObservableProperty]
    private float _klin;
    [ObservableProperty] 
    private float _chechewitsa;
    public long StripId { get; init; }
    [ObservableProperty]
    private Strip? _strip;
    [ObservableProperty] 
    private float _centralLineDeviation;

    public void OrderPoints()
    {
        ThickPoints = ThickPoints.OrderBy(p => p.Position).ToList();
    }
}