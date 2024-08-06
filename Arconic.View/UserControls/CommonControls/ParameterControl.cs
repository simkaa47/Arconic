using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;

namespace Arconic.View.UserControls.CommonControls;

public class ParameterControl : UserControl
{
    public ParameterControl()
    {
        AffectsMeasure<ParameterControl>(CommandProperty);
        AffectsMeasure<ParameterControl>(CommandParameterProperty);
        AffectsMeasure<ParameterControl>(ParamWidthProperty);
        AffectsMeasure<ParameterControl>(DescriptionInvisibleProperty);
        AffectsMeasure<ParameterControl>(CoeffProperty);
    }
    
    
    #region Command

    public ICommand? Command
    {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    // Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
    public static readonly StyledProperty<ICommand?> CommandProperty =
        AvaloniaProperty.Register<ParameterControl, ICommand?>(nameof(Command));

    #endregion

    #region CommandParameter

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    // Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
    public static readonly StyledProperty<object> CommandParameterProperty =
        AvaloniaProperty.Register<ParameterControl, object>(nameof(CommandParameter));

    #endregion

    #region ParamWidth

    public int ParamWidth
    {
        get => (int)GetValue(ParamWidthProperty);
        set => SetValue(ParamWidthProperty, value);
    }

    // Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
    public static readonly StyledProperty<int> ParamWidthProperty =
        AvaloniaProperty.Register<ParameterControl, int>(nameof(ParamWidth), 80);

    #endregion

    #region Coeff

    public float Coeff
    {
        get => (float)GetValue(CoeffProperty);
        set => SetValue(CoeffProperty, value);
    }

    // Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
    public static readonly StyledProperty<float> CoeffProperty =
        AvaloniaProperty.Register<ParameterControl, float>(nameof(Coeff), 1f);

    #endregion

    #region DescriptionInvisible

    public bool DescriptionInvisible
    {
        get => (bool)GetValue(DescriptionInvisibleProperty);
        set => SetValue(DescriptionInvisibleProperty, value);
    }

    // Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
    public static readonly StyledProperty<bool> DescriptionInvisibleProperty =
        AvaloniaProperty.Register<ParameterControl, bool>(nameof(DescriptionInvisible));

    #endregion
}