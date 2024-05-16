using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TransformDataApp.Data.Entities;

namespace TransformDataApp.Data.Context
{
    public class MyAppContext : DbContext
    {
        public DbSet<StoredEntity> StoredEntities { get; set; }

        public MyAppContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=TestAppDB; Trusted_Connection=True;");
        }
    }
}
