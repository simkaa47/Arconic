using Arconic.Core.Abstractions.Common;
using Arconic.Core.Abstractions.SteelMagazine;
using Arconic.Core.Models.PlcData;
using Arconic.Core.Models.PlcData.SteelLabel;
using Arconic.Core.Services.Plc;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;

namespace Arconic.Core.ViewModels;

public partial class SteelMagazineViewModel(PlcViewModel plcViewModel, 
    ILogger<SteelMagazineViewModel> logger, 
    IQuestionDialog questionDialog,
    MainPlcService mainPlcService,
    ISteelMagazineDialog steelMagazineDialog)
    : ObservableObject
{
    public Plc Plc { get; } = plcViewModel.Plc;
    
    private SteelMagazineItem? _selectedSteel;

    public SteelMagazineItem? SelectedSteel
    {
        get => _selectedSteel;
        set
        {
            if (value != null && SetProperty(ref _selectedSteel, value))
            {
                Plc.ControlAndIndication.SteelMagazineIndication.Index.WriteValue = (short)value.Index;
                mainPlcService.WriteParameter(Plc.ControlAndIndication.SteelMagazineIndication.Index);
            }
        }
    }
    
    [RelayCommand]
    private async Task AddSteelAsync()
    {
        Plc.ControlAndIndication.SteelMagazineIndication.PrepareToAddCommand.WriteValue = true;
        mainPlcService.WriteParameter(Plc.ControlAndIndication.SteelMagazineIndication.PrepareToAddCommand);
        if (await steelMagazineDialog.ShowDialogAsync())
        {
            logger.LogInformation("Добавление новой марки стали \"{label}\"", 
                Plc.ControlAndIndication.SteelMagazineIndication.Steel.Name.WriteValue);
            Plc.ControlAndIndication.SteelMagazineIndication.AddCommand.WriteValue = true;
            mainPlcService.WriteParameter(Plc.ControlAndIndication.SteelMagazineIndication.AddCommand);
        }
    }

    [RelayCommand]
    private async Task EditSteelAsync(object? parameter)
    {
        if (parameter is null || parameter is not SteelMagazineItem steel) return;
        Plc.ControlAndIndication.SteelMagazineIndication.Index.WriteValue = (short)steel.Index;
        mainPlcService.WriteParameter(Plc.ControlAndIndication.SteelMagazineIndication.Index);
        Plc.ControlAndIndication.SteelMagazineIndication.EditCommand.WriteValue = true;
        mainPlcService.WriteParameter(Plc.ControlAndIndication.SteelMagazineIndication.EditCommand);
        if (await steelMagazineDialog.ShowDialogAsync())
        {
            logger.LogInformation("Марки стали \"{name}\" была изменена", 
                Plc.ControlAndIndication.SteelMagazineIndication.Steel.Name.WriteValue);
            Plc.ControlAndIndication.SteelMagazineIndication.AddCommand.WriteValue = true;
            mainPlcService.WriteParameter(Plc.ControlAndIndication.SteelMagazineIndication.AddCommand);
        }
    }
    
    [RelayCommand]
    private async Task DeleteSteelAsync(object? parameter)
    {
        if (parameter is null || parameter is not SteelMagazineItem steel) return;
        var name = steel.Name.WriteValue;
        Plc.ControlAndIndication.SteelMagazineIndication.Index.WriteValue = (short)steel.Index;
        mainPlcService.WriteParameter(Plc.ControlAndIndication.SteelMagazineIndication.Index);
        if (await questionDialog.AskAsync("Удаление марки стали",
                $"Вы действительно хотите удалить марку стали \"{name}\"?"))
        {
            Plc.ControlAndIndication.SteelMagazineIndication.DeleteCommand.WriteValue = true;
            mainPlcService.WriteParameter(Plc.ControlAndIndication.SteelMagazineIndication.DeleteCommand);
            logger.LogInformation("Марки стали \"{name}\" была удалена", name);
        }
    }


}