using Microsoft.EntityFrameworkCore;

namespace WebApi
{
    public class ModelDB : DbContext
    {
        public DbSet<Person> Users { get; set; } 
        public ModelDB(DbContextOptions options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
                new Person() { Id = 1, Name = "Vasya",Age = 24 },
                new Person() { Id = 2, Name = "Boris", Age = 16 },
                new Person() { Id = 3, Name = "Artem", Age = 22 }
                );
        }
    }
}
