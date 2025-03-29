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
        public DbSet<DetailImage> DetailImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Detail>()
                .Property(d => d.Price)
                .HasColumnType("DECIMAL(18,2)");  // 6 total digits, 1 digits after the decimal
        }
    }
}
