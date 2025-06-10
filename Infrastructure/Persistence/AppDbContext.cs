using Microsoft.EntityFrameworkCore;
using movies_api.Domain.Entities;

namespace movies_api.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
