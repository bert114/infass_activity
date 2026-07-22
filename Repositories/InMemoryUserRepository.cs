using PENANO.Models;
using PENANO.Repositories;
namespace PENANO.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    // Pre-seeded with a default test account
    private static readonly List<User> _users = new()
    {
        new User { Id = 1, Username = "admin", Password = "123" }
    };

    public User? GetByUsername(string username)
    {
        return _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
    }

    public bool Create(User user)
    {
        if (GetByUsername(user.Username) != null)
            return false; // User already exists

        user.Id = _users.Count + 1;
        _users.Add(user);
        return true;
    }

    public bool ValidateUser(string username, string password)
    {
        return _users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
                            && u.Password == password);
    }
}