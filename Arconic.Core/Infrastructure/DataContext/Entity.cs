using CommunityToolkit.Mvvm.ComponentModel;

namespace Arconic.Core.Infrastructure.DataContext;

public class Entity:ObservableValidator
{
    public int Id { get; set; }
    public bool Validate()
    {
        this.ValidateAllProperties();
        return !HasErrors;
    }

    
}