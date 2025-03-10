﻿using Arconic.Core.Abstractions.DataAccess;
using Arconic.Core.Abstractions.Security;
using Arconic.Core.Models.AccessControl;
using Microsoft.Extensions.Logging;

namespace Arconic.Core.Services.Access;

public class AccessService(ILogger<AccessService> logger, 
    IRepository<User> userRepository, IPasswordHasher passwordHasher)
{
    public User? CurrentUser { get; private set; }

    public async Task AddUserAsync(User user)
    {
        try
        {
            user.Password = passwordHasher.GetHash(user.Password);
            await userRepository.AddAsync(user);
            logger.LogInformation("Пользователь с логином \"{login}\" успешно добавлен", user.Login);
        }
        catch (Exception e)
        {
            logger.LogError(e,"Ошибка добавления паользователя с логином \"{login}\"", user.Login);
        }
    }
    
    public async Task UpdateUserAsync(User user)
    {
        try
        {
            user.Password = passwordHasher.GetHash(user.Password);
            await userRepository.UpdateAsync(user);
            logger.LogInformation("Данные пользователя с логином \"{login}\" успешно обновлены", user.Login);
        }
        catch (Exception e)
        {
            logger.LogError(e,"Ошибка обновления данных пользователя с логином \"{login}\"", user.Login);
        }
    }

    public async Task<IEnumerable<User>?> GetUsers()
    {
        var users = Enumerable.Empty<User>();
        try
        {
            users = await userRepository.GetWhere(u=>u.Level != AccessLevel.Admin);
        }
        catch (Exception e)
        {
            logger.LogError(e,"Ошибка загрузки списка пользователей из базы данных");
        }
        return users;
    }

    public async Task DeleteUserAsync(User user)
    {
        try
        {
            await userRepository.DeleteAsync(user);
            logger.LogInformation("Данные пользователя с логином \"{login}\" удалены из базы данных", user.Login);
        }
        catch (Exception e)
        {
            logger.LogError(e,"Ошибка удаления пользователя с логином \"{login}\"", user.Login);
        }
    }

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
            logger.LogError(e,$"Ошибка при авторизации пользователя с логином  \"{login.LoginName}\"");
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