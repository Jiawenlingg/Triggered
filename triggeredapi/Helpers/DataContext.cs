using Microsoft.EntityFrameworkCore;

namespace triggeredapi.Helpers{

    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options){

        }

        
    }
}
