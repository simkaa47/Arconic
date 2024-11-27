using Arconic.Core.Models.Parameters;
using Arconic.Core.Services.Access;
using Arconic.Core.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace Arconic.View.UserControls.CommonControls;

public partial class ToggleSwitch : ParameterControl
{
    public ToggleSwitch()
    {
        InitializeComponent();
        AffectsMeasure<ToggleSwitch>(StateProperty);
    }
    
    #region State
    public bool State
    {
        get => (bool)GetValue(StateProperty);
        set => SetValue(StateProperty, value);
    }

// Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
    public static readonly StyledProperty<bool> StateProperty =
        AvaloniaProperty.Register<ToggleSwitch, bool>(nameof(State), defaultBindingMode:BindingMode.TwoWay);
    #endregion

    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        this.Opacity = 0.95;
        GetCommand();
        var commandParameter = GetCommandParameter();
        if (commandParameter != null)
        {
            commandParameter.WriteValue = !commandParameter.WriteValue;
            Command?.Execute(commandParameter);
        }
    }
    
    private Parameter<bool>? GetCommandParameter()
    {
        return this.CommandParameter as Parameter<bool>;
    }

    private void GetCommand()
    {
        var app = Application.Current as App;
        var accessService = app?.GetService<AccessService>();
        if (accessService is null) return;
        if (accessService.CurrentUser is null || accessService.CurrentUser.Level < this.AccessLevel)
        {
            Command = null;
            return;
        }
        if (this.Command is null)
        {
            
            Command = app?.GetService<PlcViewModel>()?.WriteParameterCommand;
        }
    }
}