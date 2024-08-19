using System.Threading.Tasks;
using Arconic.Core.Abstractions.Common;
using Arconic.Core.Models.AccessControl;
using Arconic.View.Dialogs.Access;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace Arconic.View.Dialogs.Common;

public class QuestionDialog:IQuestionDialog
{
    public async Task<bool> AskAsync(string title, string message)
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var owner = desktop.MainWindow;
            if (owner is null) return false;
            
            var window = new QuestionWindow(title, message);
            await window.ShowDialog(owner);
            return window.DialogResult;
        }

        return false;
    }
}