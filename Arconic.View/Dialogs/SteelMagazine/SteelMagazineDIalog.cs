using System.Threading.Tasks;
using Arconic.Core.Abstractions.SteelMagazine;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace Arconic.View.Dialogs.SteelMagazine;

public class SteelMagazineDIalog:ISteelMagazineDialog
{
    public async Task<bool> ShowDialogAsync()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var owner = desktop.MainWindow;
            if (owner is null) return false;
            var steelWindow = new SteelMagazineDialogWindow();
            await steelWindow.ShowDialog(owner);
            return steelWindow.DialogResult;
        }

        return false;
    }
}