using System.Threading.Tasks;
using System.Collections.Generic;
using triggeredapi.Models;
using System.Linq;
using triggeredapi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace triggeredapi.Repo
{
    public class UserDb : IUserRepository
    {
        private readonly DataContext _dbContext;

        public UserDb(DataContext dataContext)
        {
            _dbContext = dataContext;
        }

        public async Task<User> Create(User user)
        {
            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetByUserName(string username)
        {
            return await _dbContext.User.FirstOrDefaultAsync(x=> x.Username.Equals(username));
        }
        public async Task<User> GetByTelegramId(string telegramId)
        {
            return await _dbContext.User.FirstOrDefaultAsync(x=> x.TelegramId.Equals(telegramId));
        }
        public async Task<User> GetById(string id)
        {
            return await _dbContext.User.FindAsync(id);
        }
    }
}