using CommunityToolkit.Mvvm.ComponentModel;

namespace Arconic.Core.Infrastructure.DataContext;

public class Entity:ObservableObject
{
    public int Id { get; set; }
}