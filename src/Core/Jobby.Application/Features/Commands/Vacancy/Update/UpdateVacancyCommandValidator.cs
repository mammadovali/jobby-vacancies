using FluentValidation;

namespace Jobby.Application.Features.Commands.Vacancy.Update;

public class UpdateVacancyCommandValidator : AbstractValidator<UpdateVacancyCommand>
{
    public UpdateVacancyCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Kateqoriya ID-si 0-dan böyük olmalıdır.");

        RuleFor(x => x.Title)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Vakansiya başlığı boş ola bilməz.")
            .MaximumLength(200).WithMessage("Vakansiya başlığı maksimum 200 simvol ola bilər.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Vakansiya təsviri boş ola bilməz.");
    }
}