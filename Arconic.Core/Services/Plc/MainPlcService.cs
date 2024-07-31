using Arconic.Core.Models.Parameters;
using Arconic.Core.Options;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using S7.Net;

namespace Arconic.Core.Services.Plc;

public partial class MainPlcService(ILogger<MainPlcService> logger, IOptionsMonitor<PlcConnectOption> option)
    : ObservableObject
{
    private  CancellationTokenSource _cts = new CancellationTokenSource();
    private readonly S7.Net.Plc _plc = new(CpuType.S71500,option.CurrentValue.Ip,0,0);
    private List<PlcScanHelper> _ordered = new List<PlcScanHelper>();
    [ObservableProperty]
    private bool _isConnected;
    private readonly Queue<ParameterBase> _writeCommands = new();

    public void WriteParameter(ParameterBase par)
    {
        if (_plc.IsConnected)
        {
            _writeCommands.Enqueue(par);
        }
    }

    public async Task ScanPlcAsync()
    {
        _plc.ReadTimeout = 1000;
        _plc.WriteTimeout = 1000;
        
        _ordered = ParameterBase.Parameters
            .GroupBy(p => new { p.MemoryType, p.DbNum })
            .Select(g => g.OrderBy(p => p.ByteNum))
            .Select(i => new PlcScanHelper(i.ToList()))
            .ToList();
        while (true)
        {
            
            try
            {
                if (!_plc.IsConnected)
                {
                    _cts.CancelAfter(1000);
                    await _plc.OpenAsync(_cts.Token);
                    IsConnected = _plc.IsConnected;
                }

                while (_writeCommands.Count>0)
                {
                    var command = _writeCommands.Dequeue();
                    await command.WriteToPlcAsync(_plc);
                }
                foreach (var helper in _ordered)
                {
                    await ReadGroupAsync(helper);
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                if (e is OperationCanceledException cancelException)
                {
                    _cts.Dispose();
                    _cts = new CancellationTokenSource();
                }
                _plc.Close();
                IsConnected = _plc.IsConnected;
                await Task.Delay(5000);
            }
            
        }
    }

    private async Task ReadGroupAsync(PlcScanHelper helper)
    {
        _cts.CancelAfter(1000);
        var bytes = await _plc.ReadBytesAsync(helper.DataType, helper.DbNum, 0, helper.MaxByteNum, _cts.Token);
        foreach (var par in helper.Parameters)
        {
            par.GetValue(bytes);       
        }
    }
    
}