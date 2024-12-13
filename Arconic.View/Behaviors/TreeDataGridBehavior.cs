using Avalonia;
using Avalonia.Controls.Primitives;

namespace Arconic.View.Behaviors;

public class TreeDataGridBehavior : AvaloniaObject
{
    public static readonly AttachedProperty<bool> IsRowVisibleProperty =
        AvaloniaProperty.RegisterAttached<TreeDataGridBehavior, bool>(
            "IsRowVisible", typeof(TreeDataGridBehavior));

    public static void SetIsRowVisible(AvaloniaObject element, bool value) =>
        element.SetValue(IsRowVisibleProperty, value);
    public static bool GetIsRowVisible(AvaloniaObject element) =>
        element.GetValue(IsRowVisibleProperty);

    static TreeDataGridBehavior()
    {
        IsRowVisibleProperty.Changed.AddClassHandler<TreeDataGridRow, bool>(
            (row, e) => row.IsVisible = false);
    }
}