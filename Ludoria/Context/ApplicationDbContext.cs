using Ludoria.Entities;
using Ludoria.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Ludoria.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .Property(g => g.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Game>()
        .HasOne(g => g.Platform)
        .WithMany(p => p.Games)
        .HasForeignKey(g => g.PlatformId);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Category)
                .WithMany(c => c.Games)
                .HasForeignKey(g => g.CategoryId);
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
