using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesAPI.Models;

namespace MoviesAPI.EntityConfigurations
{
    public class MovieTypeEntityConfiguration: IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(k=> k.MovieID);
        }
    }
    public class RatingTypeEntityConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(k => k.RatingID);
            builder.HasIndex(k => new { k.MovieID, k.UserID }).IsUnique();
        }
    }
    public class UserTypeEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(k => k.UserID);
        }
    }
}
