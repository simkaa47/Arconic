using System.ComponentModel.DataAnnotations;
using Arconic.Core.Infrastructure.Attributes;
using Arconic.Core.Models.Parameters;
using Arconic.Core.Models.PlcData;
using Arconic.Core.Services.Plc;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Arconic.Core.ViewModels;

public partial class PlcViewModel:ObservableValidator
{
    [ObservableProperty]
    [InRange(nameof(MinValue), nameof(MaxValue))]
    private ushort _value;

    public ushort MinValue { get; } = 0;
    public ushort MaxValue { get; } = 1600;
    
    public MainPlcService MainPlcService { get; }
    public Plc Plc { get; } = new Plc();

    public PlcViewModel(MainPlcService mainPlcService)
    {
        MainPlcService = mainPlcService;
        InitAsync();
    }
    private async void  InitAsync()
    {
        await MainPlcService.ScanPlcAsync();
    }

    [RelayCommand]
    private void WriteParameter(object o)
    {
        if (o is ParameterBase par)
        {
            MainPlcService.WriteParameter(par);
            par.IsWriting = true;
        }
    }
}