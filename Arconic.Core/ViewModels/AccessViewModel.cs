using Arconic.Core.Abstractions.AccessControl;
using Arconic.Core.Abstractions.Common;
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
    private readonly IUserAddEditDIalog _userDialog;
    private readonly IQuestionDialog _questionDialog;

    public AccessViewModel(AccessService accessService,
        ILogger<AccessViewModel> logger, 
        IUserAddEditDIalog userDialog, 
        IQuestionDialog questionDialog)
    {
        _accessService = accessService;
        _logger = logger;
        _userDialog = userDialog;
        _questionDialog = questionDialog;
        InitAsync();
    }

    private async void InitAsync()
    {
        Users = await _accessService.GetUsers();
    }
    
    [ObservableProperty]
    private IEnumerable<User>? _users;
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
    [RelayCommand]
    private async Task AddUserAsync()
    {
        var user = await _userDialog.AddUser();
        if (user is not null)
        {
            await _accessService.AddUserAsync(user);
            Users = await _accessService.GetUsers();
        }
    }

    [RelayCommand]
    private async Task EditUserAsync(object? par)
    {
        if (par is not null && par is User user)
        {
            if (await _userDialog.EditUser(user))
            {
                await _accessService.UpdateUserAsync(user);
            }
        }
    }
    [RelayCommand]
    private async Task DeleteUserAsync(object? par)
    {
        if (par is not null && par is User user)
        {
            if (await _questionDialog.AskAsync("Удаление данных пользователя",
                    $"Удалить данные пользователя с логином \"{user.Login}\" из базы данных?"))
            {
                await _accessService.DeleteUserAsync(user);
                Users = await _accessService.GetUsers();
            }
        }
    }


}