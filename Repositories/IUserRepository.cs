using PENANO.Models;
using System.Collections.Concurrent;

namespace PENANO.Repositories;

public interface IUserRepository
{
    User? GetByUsername(string username);
    bool Create(User user);
    void AddInMemoryUser(string username, string password);
    bool ValidateUser(string username, string password);
}