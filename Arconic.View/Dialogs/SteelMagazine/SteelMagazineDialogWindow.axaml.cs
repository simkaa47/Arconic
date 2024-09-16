using Arconic.Core.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Arconic.View.Dialogs.SteelMagazine;

public partial class SteelMagazineDialogWindow : DialogWindow
{
    public SteelMagazineDialogWindow()
    {
        InitializeComponent();
        if (Application.Current is App app)
        {
            DataContext = app.GetService<MainViewModel>();
        }
    }
}