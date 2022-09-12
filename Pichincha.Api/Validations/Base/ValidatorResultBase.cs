using FluentValidation.Results;

namespace Pichincha.Api.Validations.Base
{
    public class ValidatorResultBase<T> : ValidationResult
    {
        public T Body { get; set; }
    }
}
