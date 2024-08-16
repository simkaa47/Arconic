using Arconic.Core.Abstractions.DataAccess;
using Arconic.Core.Abstractions.Security;
using Arconic.Core.Models.AccessControl;
using Microsoft.Extensions.Logging;

namespace Arconic.Core.Services.Access;

public class AccessService(ILogger<AccessService> logger, 
    IRepository<User> userRepository, IPasswordHasher passwordHasher)
{
    public User? CurrentUser { get; private set; }

    public async Task<bool> LoginAsync(Login login)
    {
        try
        {
            logger.LogInformation("Попытка авторизации с логином \"{login}\"", login.LoginName);
            var user = await userRepository.GetFirstWhere(u => u.Login == login.LoginName);
            if (user is not null)
            {
                var result = passwordHasher.Verify(user.Password, login.Password);
                if (result)
                {
                    CurrentUser = user;
                    logger.LogInformation("Пользователь \"{loginName}\" авторизован", user.FullName);
                    return true;
                }
            }
            login.FaliledLogin = true;
            logger.LogInformation("Попытка авторизации с логином \"{login}\" неуспешна, неправильный логин или пароль", 
                login.LoginName);
        }
        catch (Exception e)
        {
            logger.LogError($"Ошибка при авторизации пользователя с логином  \"{login.LoginName}\"",e);
        }

        return false;
    }

    public User? Logout()
    {
        if (CurrentUser is not null)
        {
            logger.LogInformation("Пользователь \"{loginName}\" вышел из системы", CurrentUser.FullName);
            CurrentUser = null;
        }

        return CurrentUser;
    }
}