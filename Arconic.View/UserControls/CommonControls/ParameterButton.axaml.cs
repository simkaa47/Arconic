using Arconic.Core.Models.Parameters;
using Arconic.Core.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Media;

namespace Arconic.View.UserControls.CommonControls;

public partial class ParameterButton : ParameterControl
{
    public ParameterButton()
    {
        InitializeComponent();
        AffectsMeasure<ParameterButton>(BehaviorProperty);
        AffectsMeasure<ParameterButton>(ButtonContentProperty);
        AffectsMeasure<ParameterButton>(StateProperty);
        
    }
    
    #region State
    public bool State
    {
        get => (bool)GetValue(StateProperty);
        set => SetValue(StateProperty, value);
    }

// Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
    public static readonly StyledProperty<bool> StateProperty =
        AvaloniaProperty.Register<ParameterButton, bool>(nameof(State), defaultBindingMode:BindingMode.TwoWay, defaultValue:false);
    #endregion
    
    #region Contect

    public Control ButtonContent
    {
        get => GetValue(ButtonContentProperty);
        set => SetValue(ButtonContentProperty, value);
    }

// Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
    public static readonly StyledProperty<Control> ButtonContentProperty =
        AvaloniaProperty.Register<ParameterButton, Control>(nameof(ButtonContent));

    #endregion
    
    #region Behavior

    public ButtonAction Behavior
    {
        get => GetValue(BehaviorProperty);
        set => SetValue(BehaviorProperty, value);
    }

// Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
    public static readonly StyledProperty<ButtonAction> BehaviorProperty =
        AvaloniaProperty.Register<ParameterButton, ButtonAction>(nameof(Behavior));

    #endregion

    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        this.Opacity = 0.95;
        GetCommand();
        var commandParameter = GetCommandParameter();
        if (commandParameter != null)
        {
            switch (Behavior)
            {
                case ButtonAction.SetFalse:
                    commandParameter.WriteValue = false;
                    break;
                case ButtonAction.SwitchToOpposite:
                    commandParameter.WriteValue = !commandParameter.WriteValue;
                    break;
                default:
                    commandParameter.WriteValue = true;
                    break;
            }
            Command?.Execute(commandParameter);
        }
    }

    private void InputElement_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        this.Opacity = 1;
        var commandParameter = GetCommandParameter();
        if (commandParameter != null && Behavior == ButtonAction.TrueIfPress)
        {
            commandParameter.WriteValue = false;
            Command?.Execute(commandParameter);
        }
    }

    private Parameter<bool>? GetCommandParameter()
    {
        return this.CommandParameter as Parameter<bool>;
    }

    private void GetCommand()
    {
        if (this.Command is null)
        {
            var app = Application.Current as App;
            Command = app?.GetService<PlcViewModel>().WriteParameterCommand;
        }
    }
}