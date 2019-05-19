using System.Collections.Generic;

namespace MoviesAPI.Validation
{
    public interface IValidator<TRequest>
    {
        IEnumerable<ValidationResult> Validate(TRequest rquest);
    }
}
