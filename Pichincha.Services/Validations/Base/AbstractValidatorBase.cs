using FluentValidation;

namespace Pichincha.Services.Validations.Base
{
    public class AbstractValidatorBase<TBody, T> : AbstractValidator<T>
    {
        private TBody? Body { get; }

        public AbstractValidatorBase(TBody? body)
        {
            Body = body;
        }

        public new async Task<ValidatorResultBase<TBody?>> ValidateAsync(T context, CancellationToken cancellation = new())
        {
            var result = await base.ValidateAsync(context, cancellation);

            return new ValidatorResultBase<TBody?>
            {
                Errors = result.Errors,
                RuleSetsExecuted = result.RuleSetsExecuted,
                Body = Body
            };
        }
    }
}
