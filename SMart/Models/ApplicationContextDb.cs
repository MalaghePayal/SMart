using Microsoft.EntityFrameworkCore;

namespace SMart.Models
{
    public class ApplicationContextDb:DbContext
    {
        public ApplicationContextDb(DbContextOptions<ApplicationContextDb> options):base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
    //Add-Migration InitIdentityContext -Context AccountContext
}
