using System.Collections.Generic;
using MoviesAPI.Helpers;
using MoviesAPI.Models;

namespace MoviesAPI.Validation.Validators
{
    public class AddMovieRatingValidator : IValidator<AddMovieRating>
    {
        public IEnumerable<ValidationResult> Validate(AddMovieRating addMovieRating)
        {
            var validationResult = new List<ValidationResult>();
            if (addMovieRating == null)
            {
                validationResult.Add(new ValidationResult
                {
                    ErrorCode = ErrorCodes.Codes.BadRequest.Invalid_AddMovieRatingRequest,
                    ErrorMessage = ErrorCodes.Messages.BadRequest.Invalid_AddMovieRatingRequest
                });
            }
            else
            {
                if (!(addMovieRating.MovieRating >= 1 && addMovieRating.MovieRating <= 5))
                {
                    validationResult.Add(new ValidationResult
                    {
                        ErrorCode = ErrorCodes.Codes.BadRequest.Invalid_AddMovieRatingRequest,
                        ErrorMessage = ErrorCodes.Messages.BadRequest.Invalid_AddMovieRatingRequest
                    });
                }
            }
            return validationResult;
        }
    }
}
