using Arconic.Core.Models.AccessControl;

namespace Arconic.Core.Abstractions.AccessControl;

public interface IUserAddEditDIalog
{
    public Task<bool> EditUser(User user);
    public Task<User?> AddUser();
}