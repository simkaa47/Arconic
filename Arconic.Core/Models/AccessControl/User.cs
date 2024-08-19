using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Arconic.Core.Infrastructure.DataContext;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Arconic.Core.Models.AccessControl;

public partial class User:Entity
{
    [MinLength(3)]
    [Required]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private string _login  = string.Empty;
    [MinLength(3)]
    [Required]
    [NotifyDataErrorInfo]
    [MaxLength(100)]
    [ObservableProperty]
    private string _lastName  = string.Empty;
    [MinLength(3)]
    [Required]
    [NotifyDataErrorInfo]
    [MaxLength(100)]
    [ObservableProperty]
    private string _firstName  = string.Empty;
    [ObservableProperty]
    private string _description  = string.Empty;
    [ObservableProperty]
    [Required]
    [NotifyDataErrorInfo]
    [MinLength(4)]
    private string _password = string.Empty;
    [ObservableProperty] 
    private AccessLevel _level;
    [NotMapped] 
    public string FullName => $"{FirstName} {LastName}";

}