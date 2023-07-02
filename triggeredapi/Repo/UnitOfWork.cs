using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using triggeredapi.Helpers;
using triggeredapi.Models;

namespace triggeredapi.Repo
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly DataContext _dbContext;
    #region Repositories
    public IRepository<Novel> Novels => 
       new GenericRepository<Novel>(_dbContext);

        UserManager<User> IUnitOfWork.Users => throw new NotImplementedException();

        public UserManager<User> Users;
       
    #endregion
    public UnitOfWork(DataContext dbContext, UserManager<User> userManager)
    {
        _dbContext = dbContext;
        Users = userManager;
    }
    public void Commit()
    {
        _dbContext.SaveChanges();
    }
    public void Dispose()
    {
        _dbContext.Dispose();
    }
    public void RejectChanges()
    {
        foreach (var entry in _dbContext.ChangeTracker.Entries()
              .Where(e => e.State != EntityState.Unchanged))
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Modified:
                case EntityState.Deleted:
                    entry.Reload();
                    break;
            }
        }
    }
    }
}