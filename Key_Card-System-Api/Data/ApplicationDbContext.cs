using Keycard_System_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Keycard_System_API.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
