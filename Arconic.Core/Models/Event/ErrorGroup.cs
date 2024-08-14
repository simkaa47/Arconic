using CommunityToolkit.Mvvm.ComponentModel;

namespace Arconic.Core.Models.Event;

public partial class ErrorGroup(string groupName):ObservableObject
{
    public string GroupName { get; } = groupName;
    [ObservableProperty]
    private List<PlcError>? _errors;
}
