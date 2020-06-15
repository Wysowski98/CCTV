using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CctvDB
{
    public class CctvDbContext: IdentityDbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Camera> Cameras { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }

        public CctvDbContext(DbContextOptions<CctvDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
