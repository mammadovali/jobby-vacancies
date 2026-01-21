using FluentValidation;

namespace Jobby.Application.Features.Commands.Question.Create
{
    public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
    {
        public CreateQuestionCommandValidator()
        {
            RuleFor(x => x.VacancyId)
                .GreaterThan(0).WithMessage("Vakansiya ID 0 dan böyük olmalıdır");

            RuleFor(x => x.Text)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Sual boş ola bilməz")
                .MaximumLength(1000).WithMessage("Sualın uzunluğu maksimum 1000 simvol ola bilər");

            RuleFor(x => x.Options)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Cavab variantları boş ola bilməz")
                .Must(x => x.Count >= 4)
                .WithMessage("Sualın ən azı 4 cavab variantı olmalıdır");

            RuleFor(x => x.Options.Count(o => o.IsCorrect))
                .Equal(1)
                .WithMessage("Sualın yalnız 1 doğru cavabı ola bilər");
        }
    }
}
