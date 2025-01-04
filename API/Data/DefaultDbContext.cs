using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class DefaultDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<AppUser> Users { get; set; }
    }
}
