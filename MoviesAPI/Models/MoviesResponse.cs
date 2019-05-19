using System.Collections.Generic;

namespace MoviesAPI.Models
{
    public class MoviesResponse
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public int RunningTime { get; set; }
        public string genres { get; set; }
        public decimal AverageRating { get; set; }
        public IEnumerable<Error> Errors { get; set; }
    }

    public class MoviesResponseList
    {
        public MoviesResponseList() => MoviesResponses = new List<MoviesResponse>();

        public List<MoviesResponse> MoviesResponses { get; set; }
    }
}
