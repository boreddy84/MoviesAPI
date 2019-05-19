using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesAPI.Models;

namespace MoviesAPI.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Context(
                serviceProvider.GetRequiredService<DbContextOptions<Context>>()))
            {
                if (context.Movies.Any() && context.Users.Any() && context.Ratings.Any())
                {
                    return;
                }

                if (!context.Movies.Any())
                {

                    context.Movies.AddRange(
                        new Movie
                        {
                            Title = "Harry Potter and the Philosopher's Stone",
                            ReleaseDate = DateTime.Parse("2001-04-11"),
                            Genre = "Fantasy",
                            PartialTitle = "Harry Potter and the Philosopher's Stone",
                            RunningTime = 180

                        },
                        new Movie
                        {
                            Title = "Harry Potter and the Chamber of Secrets",
                            ReleaseDate = DateTime.Parse("2002-03-11"),
                            Genre = "Fantasy",
                            PartialTitle = "Harry Potter and the Chamber of Secrets",
                            RunningTime = 190

                        },
                        new Movie
                        {
                            Title = "Black Panther",
                            ReleaseDate = DateTime.Parse("2018-02-16"),
                            Genre = "Action and Adventures",
                            PartialTitle = "Black Panther",
                            RunningTime = 200

                        },
                        new Movie
                        {
                            Title = "The Fast and the Furious",
                            ReleaseDate = DateTime.Parse("2001-06-22"),
                            Genre = "Action",
                            PartialTitle = "The Fast and the Furious",
                            RunningTime = 210
                        },
                         new Movie
                         {
                             Title = "Avatar",
                             ReleaseDate = DateTime.Parse("2009-12-18"),
                             Genre = "Epic science fiction",
                             PartialTitle = "Avatar",
                             RunningTime = 170
                         },
                         new Movie
                         {
                             Title = "Titanic",
                             ReleaseDate = DateTime.Parse("1997-12-19"),
                             Genre = "Romance ",
                             PartialTitle = "Titanic",
                             RunningTime = 220
                         },
                          new Movie
                          {
                              Title = "Ghostbusters",
                              ReleaseDate = DateTime.Parse("1984-3-13"),
                              Genre = "Comedy",
                              PartialTitle = "Ghostbusters",
                              RunningTime = 180
                          }
                    );
                }

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                            new User
                            {
                                UserName = "Rich"
                            },
                            new User
                            {
                                UserName = "Ram"
                            },
                            new User
                           {
                               UserName = "Rob"
                           },
                           new User
                           {
                               UserName = "Scott"
                           },
                           new User
                           {
                               UserName = "David"
                           },
                           new User
                           {
                              UserName = "Dev"
                           },
                           new User
                           {
                              UserName = "John"
                           }

                     );

                    if (!context.Ratings.Any())
                    {
                        context.Ratings.AddRange(

                            new Rating
                            {                             
                                MovieID = 1,
                                UserID = 1,
                                MovieRating = 3
                            },
                             new Rating
                             {                               
                                 MovieID = 1,
                                 UserID = 2,
                                 MovieRating = 4
                             },
                              new Rating
                              {                                
                                  MovieID = 2,
                                  UserID = 1,
                                  MovieRating = 3
                              },
                             new Rating
                             {                               
                                 MovieID = 3,
                                 UserID = 2,
                                 MovieRating = 6
                             },
                             new Rating
                             {                                
                                 MovieID = 4,
                                 UserID = 2,
                                 MovieRating = 9
                             },
                             new Rating
                             {                               
                                 MovieID = 5,
                                 UserID = 1,
                                 MovieRating = 7
                             },
                              new Rating
                              {                                 
                                  MovieID = 6,
                                  UserID = 4,
                                  MovieRating = 5
                              },
                              new Rating
                              {                                
                                  MovieID = 7,
                                  UserID = 6,
                                  MovieRating = 6
                              }

                         );
                    }
                }

                context.SaveChanges();
            }
        }
    }
}
