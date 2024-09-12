using System.Collections.ObjectModel;
using Arconic.Core.Models.PlcData.Drive;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Arconic.Core.Models.Trends;

public partial class TrendUserDto : ObservableObject
{
    [ObservableProperty]
    private MeasModes _mode;
    [ObservableProperty]
    private List<ThickPoint>? _previousScan;
    [ObservableProperty]
    private float _expectedWidth;
    [ObservableProperty]
    private float _expectedThick;
    [ObservableProperty]
    private float _centralLine;
    [ObservableProperty]
    private float _leftBorder;
    [ObservableProperty]
    private float _rightBorder;
    [ObservableProperty]
    private float _actualWidth;
    [ObservableProperty]
    private float _minThick;
    [ObservableProperty]
    private float _maxThick;
    [ObservableProperty]
    private float _stripDeviation;
    [ObservableProperty]
    private float _klin;
    [ObservableProperty]
    private float _chechecitsa;
    
    public ObservableCollection<ThickPoint> ActualScan { get; set; } = new ObservableCollection<ThickPoint>();

    public ObservableCollection<ThickPoint> Thicks { get; private set; } = new ObservableCollection<ThickPoint>();


    public void SetThicks(IEnumerable<ThickPoint> thicks)
    {
        Thicks = new ObservableCollection<ThickPoint>(thicks);
    }
}