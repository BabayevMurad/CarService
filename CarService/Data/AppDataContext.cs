using CarService.Dtos;
using CarService.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarService.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext>options)
            :base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<DetailDto> CartDetails { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Detail>()
                .Property(d => d.Price)
                .HasColumnType("DECIMAL(18,2)");

            modelBuilder.Entity<DetailDto>()
              .Property(d => d.Price)
              .HasColumnType("DECIMAL(18,2)");
        }
    }
}
