namespace MoviesAPI.Models
{
    public class Rating
    {
        public int RatingID { get; set; }
        public int UserID { get; set; }
        public int MovieID { get; set; }
        public decimal? MovieRating { get; set; }
    }
}
