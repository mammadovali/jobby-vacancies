using FluentValidation;

namespace Jobby.Application.Features.Commands.Vacancy.Create
{
    public class CreateVacancyCommandValidator : AbstractValidator<CreateVacancyCommand>
    {
        public CreateVacancyCommandValidator()
        {
            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Vakansiyanın təsviri boş ola bilməz");

            RuleFor(c => c.Title)
                .NotEmpty().WithMessage("Vakansiyanın başlığı boş ola bilməz");

            RuleFor(c => c.CategoryId)
                .NotEmpty().WithMessage("Vakansiya kateqoriyaya aid olmalıdır");
        }
    }
}
