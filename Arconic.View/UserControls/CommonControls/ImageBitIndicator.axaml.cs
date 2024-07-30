using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Arconic.View.UserControls.CommonControls;

public partial class ImageBitIndicator : UserControl
{
    public ImageBitIndicator()
    {
        InitializeComponent();
        AffectsMeasure<ImageBitIndicator>(FalseStateImageProperty);
        AffectsMeasure<ImageBitIndicator>(TrueStateImageProperty);
        AffectsMeasure<ImageBitIndicator>(StateProperty);
    }
    
    #region FalseStateImage
    public IImage FalseStateImage
    {
        get => GetValue(FalseStateImageProperty);
        set => SetValue(FalseStateImageProperty, value);
    }

// Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
    public static readonly StyledProperty<IImage> FalseStateImageProperty =
        AvaloniaProperty.Register<ImageBitIndicator, IImage>(nameof(FalseStateImage));
    #endregion
    
    #region TrueStateImage
    public IImage TrueStateImage
    {
        get => GetValue(TrueStateImageProperty);
        set => SetValue(TrueStateImageProperty, value);
    }

// Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
    public static readonly StyledProperty<IImage> TrueStateImageProperty =
        AvaloniaProperty.Register<ImageBitIndicator, IImage>(nameof(TrueStateImage));
    #endregion
    
    #region State
    public bool State
    {
        get => (bool)GetValue(StateProperty);
        set => SetValue(StateProperty, value);
    }

// Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
    public static readonly StyledProperty<bool> StateProperty =
        AvaloniaProperty.Register<ImageBitIndicator, bool>(nameof(State));
    #endregion
}