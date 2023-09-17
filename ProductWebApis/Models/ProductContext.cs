using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace ProductWebApis.Models
{
    public class ProductContext :DbContext
    {
        public DbSet<Product> Products { get; set; }
        public ProductContext(DbContextOptions<ProductContext> options):base(options)
        {
            try
            {
                var databaseCreater = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if(databaseCreater!=null)
                {
                   if(!databaseCreater.CanConnect()) databaseCreater.Create();

                    if (!databaseCreater.HasTables()) databaseCreater.CreateTables();
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }                
        }
    }
}
