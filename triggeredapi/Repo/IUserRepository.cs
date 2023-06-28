using triggeredapi.Models;

namespace triggeredapi.Repo
{
    public interface IUserRepository
    {
        Task<User> GetByUserName(string username);
        Task<User> Create(User user);
    }
}