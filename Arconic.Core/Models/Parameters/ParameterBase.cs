using CommunityToolkit.Mvvm.ComponentModel;
using S7.Net;

namespace Arconic.Core.Models.Parameters;

public abstract partial class ParameterBase:ObservableValidator
{
    public string Description { get; set; } = String.Empty;
    [ObservableProperty]
    private bool _validationOk = false;
    [ObservableProperty]
    private bool _isWriting = false;
    [ObservableProperty]
    private bool _isReadOnly = false;
    public DataType MemoryType { get; init; }
    public int DbNum { get; init; }
    public int ByteNum { get; init; }
    public int BitNum { get; set; }
    public int Length { get; set; }
    
    public int SizeBytes => GetSize();

    private int GetSize()
    {
        if (this is Parameter<bool>) return 1;
        else if (this is Parameter<string>) return Length;
        else if (this is Parameter<short> || this is Parameter<ushort>) return 2;
        else if (this is Parameter<int> || this is Parameter<uint>) return 4;
        else if (this is Parameter<float>) return 4;
        else return 0;
    }

    public void SetValue(byte[] arr, int offset)
    {
        if (this is Parameter<bool> parBool)
            parBool.Value = Conversion.SelectBit(arr[offset], BitNum);
    }

    public static List<ParameterBase> Parameters { get; } = new List<ParameterBase>();
}