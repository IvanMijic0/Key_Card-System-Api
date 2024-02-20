using Key_Card_System_Api.Models;
using Keycard_System_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Keycard_System_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Keycard> Keycards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Keycard>().ToTable("key_cards");

            base.OnModelCreating(modelBuilder);
        }
    }
}
