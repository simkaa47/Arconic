using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Services.Plc;

internal class PlcScanHelper(List<ParameterBase> parameters)
{
    public List<ParameterBase> Parameters { get; } = parameters;
    public int MinByteNum { get; } = parameters.Select(p => p.ByteNum).Min();
    public int MaxByteNum { get; } = parameters.Select(p => p.ByteNum+p.SizeBytes).Max();
    public DataType DataType { get; } = parameters.Count > 0 ? parameters[0].MemoryType : 0;
    public int DbNum { get; } = parameters.Count > 0 ? parameters[0].DbNum : 0;
}