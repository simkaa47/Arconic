using System.ComponentModel.DataAnnotations;
using Arconic.Core.Infrastructure.DataContext;

namespace Arconic.Core.Models.AccessControl;

public class User:Entity
{
    [MinLength(3)]
    [Required]
    public string Login { get; set; } = string.Empty;
    [MinLength(3)]
    [Required]
    public string LastName { get; set; } = string.Empty;
    [MinLength(3)]
    [Required]
    public string FirstName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Password { get; set; } = String.Empty;
    public AccessLevel Level { get; set; }
    
}