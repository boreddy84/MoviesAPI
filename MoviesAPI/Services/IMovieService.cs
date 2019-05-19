using System.Threading.Tasks;
using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public interface IMovieService
    {
        Task<MoviesResponseList> GetMoviesOnFilterCriteria(MovieRequest movieRequest);
        Task<MoviesResponseList> GetMoviesOnTotalUsersRating();
        Task<MoviesResponseList> GetMoviesOnUserRating(string user);
        Task<AddorUpdateMovieRatingResponse> AddorUpdateMovieRating(AddMovieRating addMovieRating);
    }
}
