namespace Arconic.Core.Models.PlcData.SingleMeasures;

public class SingleMeasuresList(int offset)
{
    public List<SingleMeasCell> Measures { get; } = 
        Enumerable.Range(0, 16).Select(i => new SingleMeasCell(offset+i*18)).ToList();
}