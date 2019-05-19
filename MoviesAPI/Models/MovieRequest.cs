namespace MoviesAPI.Models
{
    public class MovieRequest
    {
        public string Title { get; set; }      
        public int? YearOfRelease { get; set; }       
        public string genres { get; set; }
    }
}
