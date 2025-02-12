using Arconic.Core.Infrastructure.DataContext;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Arconic.Core.Models.Trends;

public partial class TrendSettings:Entity
{
    [ObservableProperty]
    private bool _averageScanIsVisible = true;
    [ObservableProperty]
    private bool _currentScanIsVisible = true;
    [ObservableProperty]
    private bool _previousScanIsVisible = true;
    [ObservableProperty]
    private string _averageScanColor = "#88ffd9";
    [ObservableProperty]
    private string _currentScanColor = "#ff3700";
    [ObservableProperty]
    private string _previousScanColor = "#ffb917";
    [ObservableProperty]
    private string _thickCurveColor = "#ff3700";
    
}