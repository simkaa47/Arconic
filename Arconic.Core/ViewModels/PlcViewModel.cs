using System.ComponentModel.DataAnnotations;
using Arconic.Core.Infrastructure.Attributes;
using Arconic.Core.Models.Event;
using Arconic.Core.Models.Parameters;
using Arconic.Core.Models.PlcData;
using Arconic.Core.Services.Events;
using Arconic.Core.Services.Plc;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;

namespace Arconic.Core.ViewModels;

public partial class PlcViewModel:ObservableValidator
{
    private readonly ILogger<PlcViewModel> _logger;

    [ObservableProperty]
    [InRange(nameof(MinValue), nameof(MaxValue))]
    private ushort _value;

    public ushort MinValue { get; } = 0;
    public ushort MaxValue { get; } = 1600;
    
    public MainPlcService MainPlcService { get; }
    public Plc Plc { get; } = new Plc();

    public PlcViewModel(MainPlcService mainPlcService, ILogger<PlcViewModel> logger)
    {
        _logger = logger;
        MainPlcService = mainPlcService;
        InitAsync();
    }
    private async void  InitAsync()
    {
        await MainPlcService.ScanPlcAsync();
    }

    [RelayCommand]
    private void  WriteParameter(object o)
    {
        if (o is ParameterBase par && MainPlcService.IsConnected)
        {
            MainPlcService.WriteParameter(par);
            _logger.LogInformation(GetMessage(par));
            par.IsWriting = true;
        }
    }

    private string GetMessage(ParameterBase par)
    {
        (string value, string writeValue) values = ("","");
        if (par is Parameter<bool> parBool)
            values = GetValuesFromParameter(parBool);
        else if (par is Parameter<short> parShort)
            values = GetValuesFromParameter(parShort);
        else if (par is Parameter<ushort> parUshort)
            values = GetValuesFromParameter(parUshort);
        else if (par is Parameter<int> parInt)
            values = GetValuesFromParameter(parInt);
        else if (par is Parameter<uint> parUint)
            values = GetValuesFromParameter(parUint);
        else if (par is Parameter<float> parFloat)
            values = GetValuesFromParameter(parFloat);
        else if (par is Parameter<double> parDouble)
            values = GetValuesFromParameter(parDouble);
        else if (par is Parameter<string> parStr)
            values = GetValuesFromParameter(parStr);
        else if (par is Parameter<DateTime> parDt)
            values = GetValuesFromParameter(parDt);

        return $"Параметр \"{par.Description}\" был изменен с \"{values.value}\" на \"{values.writeValue}\"";
    }


    (string value, string writeValue) GetValuesFromParameter<T>(Parameter<T> par) where T:IComparable
    {
        return (par.Value!.ToString()!, par.WriteValue!.ToString()!);
    }
    
}