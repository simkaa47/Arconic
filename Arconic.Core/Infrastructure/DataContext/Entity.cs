using CommunityToolkit.Mvvm.ComponentModel;

namespace Arconic.Core.Infrastructure.DataContext;

public class Entity:ObservableValidator
{
    public long Id { get; set; }
    public bool Validate()
    {
        this.ValidateAllProperties();
        return !HasErrors;
    }

    protected Entity()
    {
        this.PropertyChanged += (s, args) =>
        {
            Validate();
        };
    }
    
    

    
}