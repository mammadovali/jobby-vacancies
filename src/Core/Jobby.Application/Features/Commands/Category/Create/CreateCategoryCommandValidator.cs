using FluentValidation;

namespace Jobby.Application.Features.Commands.Category.Create
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(100)
                .WithMessage("Kateqoriya adı 100 simvoldan çox ola bilməz");
        }
    }
}
