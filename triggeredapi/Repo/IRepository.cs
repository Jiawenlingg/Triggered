namespace triggeredapi.Repo;

using Microsoft.EntityFrameworkCore;
using triggeredapi.Helpers;

    public interface IRepository<T> where T: class
    {
        IQueryable<T> Entities { get; }
        void Remove(T entity);
        void Add(T entity);
    }

    public class GenericRepository<T> : IRepository<T> where T : class
{
    private readonly DataContext _dbContext;
    private DbSet<T> _dbSet => _dbContext.Set<T>();
    public IQueryable<T> Entities => _dbSet;
    public GenericRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }
    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }
}


