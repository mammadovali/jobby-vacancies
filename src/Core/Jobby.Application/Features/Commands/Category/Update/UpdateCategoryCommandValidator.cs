using FluentValidation;

namespace Jobby.Application.Features.Commands.Category.Update
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(c => c.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Kateqoriya adı boş ola bilməz")
                .MaximumLength(100).WithMessage("Kateqoriya adı ən çox 100 simvol ola bilər");
        }
    }
}