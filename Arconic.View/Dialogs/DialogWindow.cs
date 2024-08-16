using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Arconic.View.Dialogs;

public class DialogWindow:Window
{
    protected void Accept_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
    
    protected void Cancel_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}