using FluentValidation.Results;
using Ships.DTO;

namespace Ships.API.Helpers
{
    public static class Extensions
    {
        public static IEnumerable<object> ToValidationMessage(this List<ValidationFailure> errors)
        {
            return errors.Select(x => new ValidationErrorResponse
            {
                Property = x.PropertyName,
                ErrorMessage = x.ErrorMessage
            });
        }
    }
}
