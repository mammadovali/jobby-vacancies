using FluentValidation;

namespace Jobby.Application.Features.Commands.Applicant.SubmitAnswer
{
    public class SubmitAnswerCommandValidator
    : AbstractValidator<SubmitAnswerCommand>
    {
        public SubmitAnswerCommandValidator()
        {
            RuleFor(x => x.ApplicantId)
                .GreaterThan(0).WithMessage("Applicant ID 0 ola bilməz");

            RuleFor(x => x.QuestionId)
                .GreaterThan(0).WithMessage("QuestionId ID 0 ola bilməz");
        }
    }
}
