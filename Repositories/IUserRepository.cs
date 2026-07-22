using PENANO.Models;

namespace PENANO.Repositories;

public interface IUserRepository
{
    User? GetByUsername(string username);
    bool Create(User user);
    bool ValidateUser(string username, string password);
}