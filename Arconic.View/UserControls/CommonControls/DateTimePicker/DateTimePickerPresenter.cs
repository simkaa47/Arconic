using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;

namespace Arconic.View.UserControls.CommonControls.DateTimePicker;

public class DateTimePickerPresenter : PickerPresenterBase
{
    public DateTimePickerPresenter(): base()
    {
        Value = DateTime.Now;
    }
    
    public static readonly DirectProperty<DateTimePickerPresenter, DateTime> DateOnlyProperty =
        AvaloniaProperty.RegisterDirect<DateTimePickerPresenter, DateTime>(nameof(DateOnly),
            x => x.DateOnly, (x, v) => x.DateOnly = v);

    public static readonly DirectProperty<DateTimePickerPresenter, decimal> HourProperty =
        AvaloniaProperty.RegisterDirect<DateTimePickerPresenter, decimal>(nameof(Hour),
            x => x.Hour, (x, v) => x.Hour = v);

    public static readonly DirectProperty<DateTimePickerPresenter, decimal> MinuteProperty =
        AvaloniaProperty.RegisterDirect<DateTimePickerPresenter, decimal>(nameof(Minute),
            x => x.Minute, (x, v) => x.Minute = v);

    public static readonly DirectProperty<DateTimePickerPresenter, decimal> SecondProperty =
        AvaloniaProperty.RegisterDirect<DateTimePickerPresenter, decimal>(nameof(Second),
            x => x.Second, (x, v) => x.Second = v);
    
    private DateTime _dateOnly;
    private decimal _hour;
    private decimal _minute;
    private decimal _second;
    
    public DateTime DateOnly
    {
        get => _dateOnly;
        set => SetAndRaise(DateOnlyProperty, ref _dateOnly, value);
    }

    public decimal Hour
    {
        get => _hour;
        set => SetAndRaise(HourProperty, ref _hour, value);
    }
    public decimal Minute
    {
        get => _minute;
        set => SetAndRaise(MinuteProperty, ref _minute, value);
    }
    public decimal Second
    {
        get => _second;
        set => SetAndRaise(SecondProperty, ref _second, value);
    }
    
    NumericUpDown? _nuHour;
    NumericUpDown? _nuMinute;
    NumericUpDown? _nuSecond;
    Button? _btnOk;
    Button? _btnCancel;
    
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _nuHour = e.NameScope.Get<NumericUpDown>("Hour");
        _nuMinute = e.NameScope.Get<NumericUpDown>("Minute");
        _nuSecond = e.NameScope.Get<NumericUpDown>("Second");
        _btnOk = e.NameScope.Get<Button>("BtnOK");
        _btnCancel = e.NameScope.Get<Button>("BtnCancel");

        if (_btnOk != null)
        {
            _btnOk.Click += OnAcceptButtonClicked;
        }
        if (_btnCancel != null)
        {
            _btnCancel.Click += OnDismissButtonClicked;
        }
    }
    
    private void OnDismissButtonClicked(object? sender, RoutedEventArgs e)
    {
        OnDismiss();
    }

    private void OnAcceptButtonClicked(object? sender, RoutedEventArgs e)
    {
        OnConfirmed();
    }
    
    public DateTime Value
    {
        get => new(DateOnly.Year, DateOnly.Month, DateOnly.Day, (int)Hour, (int)Minute, (int)Second);
        set
        {
            DateOnly = value.Date;
            Hour = value.Hour;
            Minute = value.Minute;
            Second = value.Second;
        }
    }

}