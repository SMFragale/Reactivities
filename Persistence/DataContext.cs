using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        // Represents table name inside the database when it gets created
        public DbSet<Activity> Activities { get; set; }
    }
}