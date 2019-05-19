using System;

namespace MoviesAPI.Models
{
    public class Movie
    {      
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string PartialTitle { get; set; }        
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public int RunningTime { get; set; }
    }
}
