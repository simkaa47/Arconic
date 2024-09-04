using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Arconic.Core.Models.Trends;

public partial class TrendUserDto:ObservableObject
{
    [ObservableProperty]
    private List<ThickPoint>? _previousScan;
    
    public ObservableCollection<ThickPoint> ActualScan { get; } = new ObservableCollection<ThickPoint>();

    public ObservableCollection<ThickPoint> Thicks { get; } = new ObservableCollection<ThickPoint>();
}