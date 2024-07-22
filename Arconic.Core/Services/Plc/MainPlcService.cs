using CommunityToolkit.Mvvm.ComponentModel;
using S7.Net;

namespace Arconic.Core.Services.Plc;

public partial class MainPlcService:ObservableObject
{
    [ObservableProperty]
    private bool _coil;
    public async Task ScanPlcAsync()
    {
        S7.Net.Plc plc = new S7.Net.Plc(CpuType.S71500,"192.168.0.1",0,0);
        try
        {
            await plc.OpenAsync();
            while (plc.IsConnected)
            {
                var bytes = await plc.ReadBytesAsync(DataType.Output, 0, 99, 2);
                Coil = S7.Net.Conversion.SelectBit(bytes[1], 1);
                
            }
            
        }
        catch (Exception e)
        {
            
        }
        plc.Close();
    }
}