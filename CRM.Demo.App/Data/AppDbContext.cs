using CRM.Demo.Kernel;
using Microsoft.EntityFrameworkCore;

namespace CRM.Demo.App
{
    public class AppDbContext : DbContext
    {
        public DbSet<Piste> Pistes { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
