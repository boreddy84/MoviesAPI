namespace MoviesAPI.Models
{

    public interface IError
    {
        string Code { get; set; }

        string Message { get; set; }

        string Field { get; set; }
    }
    public class Error : IError
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public string Field { get; set; }
    }
}
