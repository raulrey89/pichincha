using FluentValidation.Results;

namespace Pichincha.Services.Validations.Base
{
    public class ValidatorResultBase<T> : ValidationResult
    {
        public T Body { get; set; }
    }
}
