using Microsoft.EntityFrameworkCore;
using MoviesAPI.EntityConfigurations;
using MoviesAPI.Models;

namespace MoviesAPI.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MovieTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RatingTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserTypeEntityConfiguration());
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
