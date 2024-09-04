using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Measure;

public class PlcScan
{
    public PlcScan(int offset)
    {
        Points = Enumerable.Range(0, 400).Select(i => new PlcScanPoint(offset + i * 8)).ToList();
        StartPosition = new Parameter<float>("Начальная коордтината, мм", float.MinValue, float.MaxValue,
            DataType.DataBlock, 2, offset + 3200);
        EndPosition = new Parameter<float>("Конечная коордтината, мм", float.MinValue, float.MaxValue,
            DataType.DataBlock, 2, offset + 3204);
        Width = new Parameter<float>("Ширина, мм", float.MinValue, float.MaxValue,
            DataType.DataBlock, 2, offset + 3208);
        PointsNumber = new Parameter<short>("Кол-во точек", 0, short.MaxValue,
            DataType.DataBlock, 2, offset + 3212);
        ScanNumber = new Parameter<ushort>("Номер скана", 0, ushort.MaxValue,
            DataType.DataBlock, 2, offset + 3214);
        
    }

    public List<PlcScanPoint> Points { get; }
    public Parameter<float> StartPosition { get; }
    public Parameter<float> EndPosition { get; }
    public Parameter<float> Width { get; }
    public Parameter<short> PointsNumber { get; }
    public Parameter<ushort> ScanNumber { get; }
}