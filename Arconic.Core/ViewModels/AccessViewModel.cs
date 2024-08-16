using Arconic.Core.Models.AccessControl;
using Arconic.Core.Services.Access;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;

namespace Arconic.Core.ViewModels;

public partial class AccessViewModel:ObservableObject
{
    private readonly AccessService _accessService;
    private readonly ILogger<AccessViewModel> _logger;

    public AccessViewModel(AccessService accessService, ILogger<AccessViewModel> logger)
    {
        _accessService = accessService;
        _logger = logger;
    }
    
    [ObservableProperty]
    private List<User> _users;
    [ObservableProperty]
    private User? _currentUser;
    [ObservableProperty]
    private bool _isAuthorized;
    
    
    [RelayCommand]
    public async Task<bool> LoginAsync(object parameter)
    {
        if (!(parameter is Login login)) return false;
        var result = await _accessService.LoginAsync(login);
        if (result)
        {
            CurrentUser = _accessService.CurrentUser;
            IsAuthorized = true;
            return true;
        }
        return false;
    }
    
    [RelayCommand]
    private void Logout()
    {
        CurrentUser = _accessService.Logout();
        IsAuthorized = false;
    }


}