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
    }
}
