using Microsoft.EntityFrameworkCore;

namespace JournalsServer.Model
{
    public class AppDbContext : DbContext
    {
        public static AppDbContext db = new AppDbContext();

        public DbSet<User> Users { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Registration> Registrations { get; set; }

        public DbSet<JournalShablons> JournalShablons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite("Filename=JournalDB.db");
        }
    }
}
