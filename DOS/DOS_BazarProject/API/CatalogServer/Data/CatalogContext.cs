using CatalogServer.Model;
using Microsoft.EntityFrameworkCore;

namespace CatalogServer.Data
{
    public class CatalogContext : DbContext //this class implement the DbContext which is a way to help us to deal with the database using 
        //classes instead of querys  
    {
        public DbSet<Book> Catalogs { get; set; }
        public CatalogContext(DbContextOptions<CatalogContext> opt) : base(opt)
        {
        }
        
    }
}
