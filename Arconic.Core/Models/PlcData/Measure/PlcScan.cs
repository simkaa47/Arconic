using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Measure;

public class PlcScan
{
    public PlcScan(int offset)
    {
        Points = Enumerable.Range(0, 600).Select(i => new PlcScanPoint(offset + i * 12)).ToList();
        StartPosition = new Parameter<float>("Начальная коордтината, мм", float.MinValue, float.MaxValue,
            DataType.DataBlock, 83, offset + 7200);
        EndPosition = new Parameter<float>("Конечная коордтината, мм", float.MinValue, float.MaxValue,
            DataType.DataBlock, 83, offset + 7204);
        Width = new Parameter<float>("Ширина, мм", float.MinValue, float.MaxValue,
            DataType.DataBlock, 83, offset + 7208);
        PointsNumber = new Parameter<short>("Кол-во точек", 0, short.MaxValue,
            DataType.DataBlock, 83, offset + 7212);
        ScanNumber = new Parameter<ushort>("Номер скана", 0, ushort.MaxValue,
            DataType.DataBlock, 83, offset + 7214);
        Klin = new Parameter<float>("Клин, мкм", float.MinValue, float.MaxValue,
            DataType.DataBlock, 83, offset + 7216);
        Chehevitsa = new Parameter<float>("Чечевица, мкм", float.MinValue, float.MaxValue,
            DataType.DataBlock, 83, offset + 7220);
        
    }

    public List<PlcScanPoint> Points { get; }
    public Parameter<float> StartPosition { get; }
    public Parameter<float> EndPosition { get; }
    public Parameter<float> Width { get; }
    public Parameter<short> PointsNumber { get; }
    public Parameter<ushort> ScanNumber { get; }
    
    public Parameter<float> Klin { get; }
    
    public Parameter<float> Chehevitsa { get; }
}