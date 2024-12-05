using Arconic.Core.Models.PlcData.Drive;
using Arconic.Core.Models.Trends;

namespace Arconic.Core.Abstractions.Trends;

public interface ITrendUserDto
{
    public void ReInit(MeasModes mode = MeasModes.ForwRevers,
        float expectedWidth = 0,
        float expectedThick = 0,
        float leftBorder = 0,
        float rightBorder = 0,
        float centralLine = 0);

    public void Recalculate(float stripDeviation = 0, 
        float maxThick = 0, 
        float minThick = 0, 
        float actualWidth = 0,
        float klin = 0,
        float chechevitsa = 0);
    public MeasModes Mode { get; }
    
    public void SetPreviousScan(List<ThickPoint>? thickPoints);

    public void SetActualScan(List<ThickPoint>? thickPoints);
    public void SetTimeCurve(List<ThickPoint>? thickPoints);
    public void ClearActualScan();
    public float ExpectedWidth { get; }
    public float ExpectedThick { get;  }
    public float LeftBorder { get; }
    public float CentralLine { get; }
    public float RightBorder { get; }
    public float ActualWidth { get; }
    public float MinThick { get; }
    public float MaxThick { get; }
    public float StripDeviation { get; }
    public float Klin { get; set; }
    public float Chechevitsa { get; }

    public void AddDateTimeThick(ThickPoint? point);
    public void AddPointToScan(ThickPoint? point);

}