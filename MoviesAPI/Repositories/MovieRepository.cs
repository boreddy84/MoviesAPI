using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
using MoviesAPI.Extensions;
using MoviesAPI.Models;

namespace MoviesAPI.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly Context _context;
        public MovieRepository(Context context)
        {
            _context = context;
        }

        public async Task<AddorUpdateMovieRatingResponse> AddorUpdateMovieRating(AddMovieRating addMovieRating)
        {
            var addorUpdateMovieRatingResponse = new AddorUpdateMovieRatingResponse();
            try
            {
                var movieID = _context.Movies.FirstOrDefault(m => string.Equals(m.Title, addMovieRating.Title, StringComparison.InvariantCultureIgnoreCase))?.MovieID;
                var userID = _context.Users.FirstOrDefault(u => string.Equals(u.UserName, addMovieRating.UserName, StringComparison.InvariantCultureIgnoreCase))?.UserID;

                if (movieID == null || userID == null)
                {
                    addorUpdateMovieRatingResponse.IsMovieorUserNotFound = true;
                    return addorUpdateMovieRatingResponse;
                }
                var movieResult = _context.Ratings.FirstOrDefault(x => x.MovieID == movieID && x.UserID == userID);
                var result = 0;
                if (movieResult == null)
                {
                    _context.Ratings.Add(new Rating { MovieID = (int)movieID, UserID = (int)userID, MovieRating = addMovieRating.MovieRating });
                    result = await _context.SaveChangesAsync();
                }
                else
                {
                    if (movieResult.MovieRating != addMovieRating.MovieRating)
                    {
                        movieResult.MovieRating = addMovieRating.MovieRating;
                        result = await _context.SaveChangesAsync();
                    }
                    else
                    {
                        result = 1;
                    }
                }

                addorUpdateMovieRatingResponse.Success = result > 0 ? true : false;
            }
            catch
            {
                addorUpdateMovieRatingResponse.Success = false;
            }
            return addorUpdateMovieRatingResponse;
        }

        public async Task<MoviesResponseList> GetMoviesOnFilterCriteria(MovieRequest movieRequest)
        {
            var moviesResponseList = new MoviesResponseList();
            List<string> genres = null;
            if (!string.IsNullOrEmpty(movieRequest.genres.Trim()))
            {
                genres = movieRequest.genres.Trim().ToUpper().Split(',').ToList();
            }
            IQueryable<Movie> movies = _context.Movies;
            if (!string.IsNullOrEmpty(movieRequest.Title))
            {
                movies = movies.Where(x => String.Equals(x.Title, movieRequest.Title, StringComparison.CurrentCultureIgnoreCase));
            }
            if (movieRequest.YearOfRelease.HasValue)
            {
                movies = movies.Where(x => x.ReleaseDate.Year == movieRequest.YearOfRelease.Value);
            }

            if (genres != null && genres.Count > 0)
            {
                movies = movies.Where(x => genres.Any(key => x.Genre.ToUpper().Contains(key)));
            }

            var result = await GetMoviesResponse(movies);
            moviesResponseList.MoviesResponses = result;
            return moviesResponseList;
        }

        public async Task<MoviesResponseList> GetMoviesOnTotalUsersRating()
        {
            var moviesResponseList = new MoviesResponseList();
            var result = await GetMoviesResponse(_context.Movies);
            result = result.OrderByDescending(x => x.AverageRating).ThenBy(x => x.Title).Take(5).ToList();
            moviesResponseList.MoviesResponses = result;
            return moviesResponseList;
        }

        private async Task<List<MoviesResponse>> GetMoviesResponse(IQueryable<Movie> movies)
        {
            var result = await (from m in movies
                                from r in _context.Ratings.Where(x => x.MovieID == m.MovieID).DefaultIfEmpty()
                                group new { m, r } by new { m.MovieID, m.Title, m.ReleaseDate, m.RunningTime, m.Genre } into g
                                select new MoviesResponse()
                                {
                                    ID = g.Key.MovieID,
                                    Title = g.Key.Title,
                                    YearOfRelease = g.Key.ReleaseDate.Year,
                                    RunningTime = g.Key.RunningTime,
                                    genres = g.Key.Genre,
                                    AverageRating = g.Average(x => x.r.MovieRating).RoundToNearestHalf()
                                }).ToListAsync();
            return result;

        }

        public async Task<MoviesResponseList> GetMoviesOnUserRating(string user)
        {
            var moviesResponseList = new MoviesResponseList();
            var result = await (from m in _context.Movies
                                join r in _context.Ratings on m.MovieID equals r.MovieID
                                join usr in _context.Users on r.UserID equals usr.UserID
                                where usr.UserName.Equals(user, StringComparison.OrdinalIgnoreCase)
                                select new MoviesResponse()
                                {
                                    ID = m.MovieID,
                                    Title = m.Title,
                                    YearOfRelease = m.ReleaseDate.Year,
                                    RunningTime = m.RunningTime,
                                    genres = m.Genre,
                                    AverageRating = r.MovieRating.RoundToNearestHalf()
                                }).ToListAsync();

            result = result.Take(5).OrderByDescending(x => x.AverageRating).ThenBy(x => x.Title).ToList();
            moviesResponseList.MoviesResponses = result;
            return moviesResponseList;
        }
    }
}
