using Arconic.Core.Models.Parameters;
using Arconic.Core.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using S7.Net;

namespace Arconic.Core.Services.Plc;

public class MainPlcService
{
    private readonly ILogger<MainPlcService> _logger;
    private readonly IOptionsMonitor<PlcConnectOption> _option;
    private readonly byte[] _bytes = new byte[10000];
    private List<IOrderedEnumerable<ParameterBase>> ordered = new List<IOrderedEnumerable<ParameterBase>>();

    public MainPlcService(ILogger<MainPlcService> logger, IOptionsMonitor<PlcConnectOption> option)
    {
        _logger = logger;
        _option = option;
    }
    public async Task ScanPlcAsync()
    {
        var ordered = ParameterBase.Parameters
            .GroupBy(p => new { p.MemoryType, p.DbNum })
            .Select(g => g.OrderBy(p => p.ByteNum)).ToList();
        while (true)
        {
            S7.Net.Plc plc = new S7.Net.Plc(CpuType.S71500,_option.CurrentValue.Ip,0,0);
            await plc.OpenAsync();
            try
            {
                foreach (var group in ordered)
                {
                    await ReadGroupAsync(group.ToList());
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            plc.Close();
        }
    }

    private async Task ReadGroupAsync(List<ParameterBase> parameters)
    {
        
    }
    
    
}