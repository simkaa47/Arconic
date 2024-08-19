using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Arconic.View.Dialogs.Common;

public partial class QuestionWindow : DialogWindow
{
    public string Message { get;  set; }
    public string  UserTitle { get;  set;  }

    public QuestionWindow(string title, string message)
    {
        UserTitle = title;
        Message = message;
        InitializeComponent();
    }
    
    
}