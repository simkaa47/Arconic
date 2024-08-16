using Arconic.Core.Models.AccessControl;
using Arconic.Core.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Arconic.View.Dialogs.Access;

public partial class AccessDialogWindow : DialogWindow
{
    public AccessDialogWindow()
    {
        InitializeComponent();
        var login = new Login();
        Login = login;
        DataContext = login;
        Login.Validate();
    }
    Login Login { get;}
    
    private async void Login_Click(object sender, RoutedEventArgs e)
    {
        if (Application.Current is App app)
        {
            if (!Login.Validate()) return;
            var vm = app.GetService<AccessViewModel>();
            if (vm != null)
            {
                var result = await vm.LoginAsync(Login);
                if (result)
                {
                    this.Close();
                }
            }
        }
    }

    
}