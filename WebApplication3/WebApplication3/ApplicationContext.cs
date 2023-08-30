using Microsoft.EntityFrameworkCore;

namespace WebApplication3
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "af", Price = 301 },
                new User { Id = 2, Name = "qwf", Price = 220 },
                new User { Id = 3, Name = "qwt", Price = 252 }

                );
        }
    }
}
