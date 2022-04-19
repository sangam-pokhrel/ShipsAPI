using Microsoft.EntityFrameworkCore;
using Ships.Domain.Entities;

namespace Ships.Domain
{
    public class ShipsDbContext : DbContext
    {
        public ShipsDbContext(DbContextOptions<ShipsDbContext> options) : base(options)
        {
        }

        public DbSet<Ship> Ships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ship>().HasKey(x => x.Code);
            modelBuilder.Entity<Ship>().Property(x => x.Code).HasMaxLength(12);
            modelBuilder.Entity<Ship>().Property(x => x.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Ship>().Property(x => x.LengthInMeters).IsRequired();
            modelBuilder.Entity<Ship>().Property(x => x.WidthInMeters).IsRequired();
        }
    }
}
