using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Arconic.View.Dialogs;

public class DialogWindow:Window
{
    public bool DialogResult { get; private set; }
    protected void Accept_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        this.Close();
    }
    
    protected void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        this.Close();
        
    }
}