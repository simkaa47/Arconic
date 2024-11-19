using System.Collections.Generic;
using System.Threading.Tasks;
using Arconic.Core.Abstractions.FileAccess;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;

namespace Arconic.View.Dialogs.FileAccess;

public class AvaloniaFileDialog:IFileDialog
{
    public async Task<string> GetDirectory()
    {
        if (!(Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop))
        {
            return string.Empty;
        }
        var topLevel = TopLevel.GetTopLevel(desktop.MainWindow);   
        if (topLevel == null) return string.Empty;
        var files = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            AllowMultiple = false
        });

        if (files.Count > 0)
        {
            return files[0].Path.LocalPath;
        }
        return string.Empty;
    }

    public async Task<string> GetFile()
    {
        if (!(Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop))
        {
            return string.Empty;
        }
        var topLevel = TopLevel.GetTopLevel(desktop.MainWindow);
        if (topLevel == null) return string.Empty;
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            AllowMultiple = false
        });

        if (files.Count > 0)
        {
            return files[0].Path.LocalPath;
        }
        return string.Empty;
    }
}