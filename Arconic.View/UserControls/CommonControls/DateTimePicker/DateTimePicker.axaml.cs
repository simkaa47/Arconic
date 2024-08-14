using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;

namespace Arconic.View.UserControls.CommonControls.DateTimePicker;

public partial class DateTimePicker : UserControl
{
    public DateTimePicker()
    {
        InitializeComponent();
        var pickButton = this.FindControl<Button>("PickButton");
        _popup = this.FindControl<Popup>("Popup");
        _dateTimePickerPresenter = this.FindControl<DateTimePickerPresenter>("PopPresenter");
        _dateTimePickerPresenter!.Confirmed += PopPresenter_Confirmed;
        _dateTimePickerPresenter.Dismissed += PopPresenter_Dismissed;
        pickButton!.Tapped += BtnPicker_Tapped;
        AffectsMeasure<DateTimePicker>(DateTimeProperty);
        AffectsMeasure<DateTimePicker>(CommandParameterProperty);
        AffectsMeasure<DateTimePicker>(CommandProperty);
        AffectsMeasure<DateTimePicker>(DescriptionProperty);
    }

    private readonly Popup? _popup;
    private readonly DateTimePickerPresenter? _dateTimePickerPresenter;
    
    private void PopPresenter_Dismissed(object? sender, System.EventArgs e)
    {
        _popup!.Close();
    }
    
    private void PopPresenter_Confirmed(object? sender, System.EventArgs e)
    {
        DateTime = _dateTimePickerPresenter!.Value;
        _popup?.Close();
        if (Command is not null)
        {
            Command.Execute(CommandParameter);
        }
    }
    
    private void BtnPicker_Tapped(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _popup!.IsOpen = true;
    }
    
    #region dt
    public DateTime DateTime
    {
        get => GetValue(DateTimeProperty);
        set => SetValue(DateTimeProperty, value);
    }

// Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
    public static readonly StyledProperty<DateTime> DateTimeProperty =
        AvaloniaProperty.Register<DateTimePicker, DateTime>(nameof(DateTime), defaultBindingMode:BindingMode.TwoWay);
    #endregion

    #region Command
    public ICommand? Command
    {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

// Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
    public static readonly StyledProperty<ICommand?> CommandProperty =
        AvaloniaProperty.Register<DateTimePicker, ICommand?>(nameof(Command));
    #endregion

    #region CommandParameter
    public object? CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

// Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
    public static readonly StyledProperty<object?> CommandParameterProperty =
        AvaloniaProperty.Register<DateTimePicker, object?>(nameof(CommandParameter));
    #endregion

    #region Description
    public string Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

// Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
    public static readonly StyledProperty<string> DescriptionProperty =
        AvaloniaProperty.Register<DateTimePicker, string>(nameof(Description));
    #endregion
}