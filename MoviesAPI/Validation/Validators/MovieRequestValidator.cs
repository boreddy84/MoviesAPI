using System.Collections.Generic;
using MoviesAPI.Helpers;
using MoviesAPI.Models;

namespace MoviesAPI.Validation.Validators
{
    public class MovieRequestValidator : IValidator<MovieRequest>
    {
        public IEnumerable<ValidationResult> Validate(MovieRequest movieRequest)
        {
            var validationResult = new List<ValidationResult>();
            if(movieRequest==null)
            {
                validationResult.Add(new ValidationResult
                {
                    ErrorCode=ErrorCodes.Codes.BadRequest.Invalid_MovieRequest,
                    ErrorMessage= ErrorCodes.Messages.BadRequest.Invalid_MovieRequest
                });
            }
            else
            {
                if(string.IsNullOrEmpty(movieRequest.Title) && !movieRequest.YearOfRelease.HasValue && string.IsNullOrEmpty(movieRequest.genres))
                {
                    validationResult.Add(new ValidationResult
                    {
                        ErrorCode = ErrorCodes.Codes.BadRequest.Invalid_MovieRequest,
                        ErrorMessage = ErrorCodes.Messages.BadRequest.Invalid_MovieRequest
                    });
                }
            }

            return validationResult;
        }
    }
}
