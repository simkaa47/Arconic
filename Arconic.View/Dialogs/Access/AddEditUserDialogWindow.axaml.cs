using Arconic.Core.Models.AccessControl;

namespace Arconic.View.Dialogs.Access;

public partial class AddEditUserDialogWindow : DialogWindow
{
    public AddEditUserDialogWindow(User user)
    {
        InitializeComponent();
        user.Validate();
        this.DataContext = user;
    }
}