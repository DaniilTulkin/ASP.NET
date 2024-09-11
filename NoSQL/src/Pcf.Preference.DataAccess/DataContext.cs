using Microsoft.EntityFrameworkCore;

namespace Pcf.Preference.DataAccess
{
    public class DataContext
        : DbContext
    {
        public DbSet<Core.Domain.Preference> Partners { get; set; }

        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
