using FluentValidation;
using odata.models;

namespace odata.Validators.V1
{
    internal sealed class DegreeValidator : AbstractValidator<Degree>
    {
        public DegreeValidator()
            : base()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Name).MaximumLength(2000);
        }
    }
}