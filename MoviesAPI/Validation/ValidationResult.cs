namespace MoviesAPI.Validation
{
    public class ValidationResult
    {
        public ValidationResult() { }
        public ValidationResult(string errorCode,string errorMessage,string field)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Field = field;
        }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Field { get; set; }
    }
}
