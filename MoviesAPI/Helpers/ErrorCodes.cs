namespace MoviesAPI.Helpers
{
    public class ErrorCodes
    {
        public class Codes
        {
            public class NotFound
            {
                public const string NotFound_MovieorUser = "NotFound_MovieorUser";
                public const string NotFound_Movie = "NotFound_Movie";
            }
            public class BadRequest
            {
                public const string Invalid_MovieRequest = "Invalid_MovieRequest";
                public const string Invalid_AddMovieRatingRequest = "Invalid_AddMovieRatingRequest";
            }
            public class ServerError
            {
                public const string ServerError_UpsertFailedForRating = "ServerError_UpsertFailedForRating";
                public const string ServerError_exception = "Internal Server Error";
            }
        }

        public class Messages
        {
            public class NotFound
            {
                public const string NotFound_MovieorUser = "No Record could be found matching the Movie or User ";
                public const string NotFound_Movie = "No Record could be found matching the given criteria";
            }
            public class BadRequest
            {
                public const string Invalid_MovieRequest = "Invalid Movie Request";
                public const string Invalid_AddMovieRatingRequest = "Invalid AddMovie Rating Request";
            }
            public class ServerError
            {
                public const string ServerError_UpsertFailedForRating = "Movie Rating could not be updated";
                public const string ServerError_exception = "Internal Server Error";
            }
        }
    }
}
