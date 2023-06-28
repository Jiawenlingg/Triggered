using triggeredapi.Models;

namespace triggeredapi.Repo
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();
        public Task<User> Create(User user)
        {
            _users.Add(user);
            return Task.FromResult(user);
        }

        public Task<User> GetByUserName(string username)
        {
            return Task.FromResult(_users.FirstOrDefault(x=> x.Username.Equals(username)));
        }
    }
}