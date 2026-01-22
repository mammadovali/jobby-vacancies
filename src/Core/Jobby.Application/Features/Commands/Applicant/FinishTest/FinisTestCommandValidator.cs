using FluentValidation;

namespace Jobby.Application.Features.Commands.Applicant.FinishTest
{
    public class FinisTestCommandValidator : AbstractValidator<FinishTestCommand>
    {
        public FinisTestCommandValidator()
        {
            RuleFor(x => x.ApplicantId)
                .GreaterThan(0).WithMessage("Applicant ID 0 ola bilməz");
        }
    }
}
