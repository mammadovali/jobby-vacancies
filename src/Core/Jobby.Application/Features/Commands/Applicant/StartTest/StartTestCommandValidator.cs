using FluentValidation;

namespace Jobby.Application.Features.Commands.Applicant.StartTest
{
    public class StartTestCommandValidator : AbstractValidator<StartTestCommand>
    {
        public StartTestCommandValidator()
        {
            RuleFor(x => x.ApplicantId)
                .GreaterThan(0)
                .WithMessage("ApplicantId 0 ola bilməz");
        }
    }
}
