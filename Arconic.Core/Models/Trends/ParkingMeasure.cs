using System.Collections.ObjectModel;
using Arconic.Core.Infrastructure.DataContext;

namespace Arconic.Core.Models.Trends;

public class ParkingMeasure : Entity
{
    public ObservableCollection<TimePoint> Points { get; set; } = new ObservableCollection<TimePoint>();
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    public float ExpectedThick { get; set; }
    
}