using Microsoft.EntityFrameworkCore;
using Web.Test.Entities;

namespace Web.Test.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) 
        {

        }

        public DbSet<Inventario> Inventarios { get; set; } 
    }
}
