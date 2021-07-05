using Microsoft.EntityFrameworkCore;

namespace JournalsServer.Model
{
    public class AppDbContext : DbContext
    {
        public static AppDbContext db = new AppDbContext();

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite("Filename=JournalDB.db");
        }
    }
}
