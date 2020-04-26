using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CctvDB
{
    public class CctvDbContext: IdentityDbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Cctv> Cctvs { get; set; }
        public DbSet<Videos> Video { get; set; }

        public CctvDbContext(DbContextOptions<CctvDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cctv>().HasOne(x => x.Client).WithMany(y => y.FavouriteCctvs).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Client>().HasMany(x => x.FavouriteCctvs);
        }
    }
}
