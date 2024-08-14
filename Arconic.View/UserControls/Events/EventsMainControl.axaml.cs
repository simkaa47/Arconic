using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Arconic.View.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace Arconic.View.UserControls.Events;

public partial class EventsMainControl : UserControl
{
    private readonly TreeDataGrid? _dataGreed;
    public EventsMainControl()
    {
        InitializeComponent();
        var app = Application.Current as App;
        this.DataContext = app?.GetService<EventsViewModel>();
        _dataGreed = this.Get<TreeDataGrid>("TreeDataGrid");
        _dataGreed.Loaded += (s, args) =>
        {
            var firstColumn = Children<TreeDataGridColumnHeader>(_dataGreed).First();
            firstColumn.RaiseEvent(new RoutedEventArgs { RoutedEvent = Button.ClickEvent });
            firstColumn.RaiseEvent(new RoutedEventArgs { RoutedEvent = Button.ClickEvent });
        };
    }

   

    IEnumerable<T> Children<T>(Visual parent)
    {
        foreach (var child in parent.GetVisualChildren())
        {
            if (child is T target)
                yield return target;

            foreach (T item in Children<T>(child))
                yield return item;
        }
    }
}