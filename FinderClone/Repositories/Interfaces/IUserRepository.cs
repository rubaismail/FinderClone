using FinderClone.Models;

namespace FinderClone.Repositories.Interfaces;

public interface IUserRepository
{
    Task <bool> RegisterUser (User user);
    Task <string?> AuthenticateUser(User user);
}