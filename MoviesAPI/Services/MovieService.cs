using System;
using System.Linq;
using System.Threading.Tasks;
using MoviesAPI.Helpers;
using MoviesAPI.Models;
using MoviesAPI.Repositories;
using MoviesAPI.Validation;

namespace MoviesAPI.Services
{
    public class MovieService : IMovieService
    {
        private IMovieRepository _movieRepository;
        private IValidator<MovieRequest> _movieRequestValidator;
        private IValidator<AddMovieRating> _addMovieRatingValidator;
        public MovieService(IMovieRepository movieRepository, IValidator<MovieRequest> movieRequestValidator, IValidator<AddMovieRating> addMovieRatingValidator)
        {
            _movieRepository = movieRepository;
            _movieRequestValidator = movieRequestValidator;
            _addMovieRatingValidator = addMovieRatingValidator;
        }

        public async Task<AddorUpdateMovieRatingResponse> AddorUpdateMovieRating(AddMovieRating addMovieRating)
        {
            var addorUpdateMovieRatingResponse = new AddorUpdateMovieRatingResponse();
            var validateResult = _addMovieRatingValidator.Validate(addMovieRating);
            if (validateResult.Any())
            {
                addorUpdateMovieRatingResponse.Errors = validateResult.Select(x => new Error { Code = x.ErrorCode, Message = x.ErrorMessage });
                return addorUpdateMovieRatingResponse;
            }
            addorUpdateMovieRatingResponse = await _movieRepository.AddorUpdateMovieRating(addMovieRating);
            if (addorUpdateMovieRatingResponse.IsMovieorUserNotFound)
            {
                addorUpdateMovieRatingResponse.Errors = new[] { new Error { Code = ErrorCodes.Codes.NotFound.NotFound_MovieorUser, Message = ErrorCodes.Messages.NotFound.NotFound_MovieorUser } };
                return addorUpdateMovieRatingResponse;
            }
            if (!addorUpdateMovieRatingResponse.Success)
            {
                addorUpdateMovieRatingResponse.Errors = new[] { new Error { Code = ErrorCodes.Codes.ServerError.ServerError_UpsertFailedForRating, Message = ErrorCodes.Messages.ServerError.ServerError_UpsertFailedForRating } };
            }
            return addorUpdateMovieRatingResponse;

        }

        public async Task<MoviesResponseList> GetMoviesOnFilterCriteria(MovieRequest movieRequest)
        {
            var moviesResponseList = new MoviesResponseList();
            try
            {
                var validateResult = _movieRequestValidator.Validate(movieRequest);
                if (validateResult.Any())
                {
                    moviesResponseList.MoviesResponses.Add(new MoviesResponse { Errors = validateResult.Select(x => new Error { Code = x.ErrorCode, Message = x.ErrorMessage }) });
                    return moviesResponseList;
                }
                moviesResponseList = await _movieRepository.GetMoviesOnFilterCriteria(movieRequest);
                if (moviesResponseList.MoviesResponses == null || (moviesResponseList.MoviesResponses != null && moviesResponseList.MoviesResponses.Count == 0))
                {
                    moviesResponseList.MoviesResponses.Add(new MoviesResponse { Errors = new[] { new Error { Code = ErrorCodes.Codes.NotFound.NotFound_Movie, Message = ErrorCodes.Messages.NotFound.NotFound_Movie } } });
                }
            }
            catch (Exception ex)
            {
                moviesResponseList.MoviesResponses.Add(new MoviesResponse { Errors = new[] { new Error { Code = ErrorCodes.Codes.ServerError.ServerError_exception, Message = ex.ToString() } } });
            }
            return moviesResponseList;
        }

        public async Task<MoviesResponseList> GetMoviesOnTotalUsersRating()
        {
            var moviesResponseList = new MoviesResponseList();
            moviesResponseList = await _movieRepository.GetMoviesOnTotalUsersRating();
            if (moviesResponseList.MoviesResponses == null || (moviesResponseList.MoviesResponses != null && moviesResponseList.MoviesResponses.Count == 0))
            {
                moviesResponseList.MoviesResponses.Add(new MoviesResponse { Errors = new[] { new Error { Code = ErrorCodes.Codes.NotFound.NotFound_Movie, Message = ErrorCodes.Messages.NotFound.NotFound_Movie } } });
            }
            return moviesResponseList;
        }

        public async Task<MoviesResponseList> GetMoviesOnUserRating(string user)
        {
            var moviesResponseList = new MoviesResponseList();
            moviesResponseList = await _movieRepository.GetMoviesOnUserRating(user);
            if (moviesResponseList.MoviesResponses == null || (moviesResponseList.MoviesResponses != null && moviesResponseList.MoviesResponses.Count == 0))
            {
                moviesResponseList.MoviesResponses.Add(new MoviesResponse { Errors = new[] { new Error { Code = ErrorCodes.Codes.NotFound.NotFound_Movie, Message = ErrorCodes.Messages.NotFound.NotFound_Movie } } });
            }
            return moviesResponseList;
        }
    }
}
