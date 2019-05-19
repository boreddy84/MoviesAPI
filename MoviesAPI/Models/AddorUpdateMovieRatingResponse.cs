using System.Collections.Generic;

namespace MoviesAPI.Models
{
    public class AddorUpdateMovieRatingResponse
    {
        public bool Success { get; set; }
        public bool IsMovieorUserNotFound { get; set; }       
        public IEnumerable<Error> Errors { get; set; }
    }
}
