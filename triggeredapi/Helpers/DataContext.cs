using Microsoft.EntityFrameworkCore;
using triggeredapi.Models;

namespace triggeredapi.Helpers{

    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options){

        }

        public DbSet<User> User {get; set;}
        public DbSet<Novel> Novel {get; set;}

        
    }
}
