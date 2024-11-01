using System.Threading.Tasks;
using Arconic.Core.Abstractions.AccessControl;
using Arconic.Core.Abstractions.Security;
using Arconic.Core.Models.AccessControl;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Mapster;

namespace Arconic.View.Dialogs.Access;

public class AddEditUserDialog : IUserAddEditDIalog
{
    public async Task<bool> EditUser(User user)
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var owner = desktop.MainWindow;
            if (owner is null) return false;
            var userToView = user.Adapt<User>();
            userToView.Password = "12345";
            var window = new AddEditUserDialogWindow(userToView);
            await window.ShowDialog(owner);
            if (window.DialogResult && !userToView.HasErrors)
            {
                userToView.Adapt<User, User>(user);
                return true;
            }
            
        }
        return false;
    }

    public async Task<User?> AddUser()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var owner = desktop.MainWindow;
            if (owner is null) return null;
            var user = new User();
            var window = new AddEditUserDialogWindow(user);
            await window.ShowDialog(owner);
            return window.DialogResult ? user : null;
        }

        return null;
    }
}