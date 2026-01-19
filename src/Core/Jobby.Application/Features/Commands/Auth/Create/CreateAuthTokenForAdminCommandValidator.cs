using FluentValidation;

namespace Jobby.Application.Features.Commands.Auth.Create
{
    public class CreateAuthTokenForAdminCommandValidator : AbstractValidator<CreateAuthTokenForAdminCommand>
    {
        public CreateAuthTokenForAdminCommandValidator() : base()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("Email boş ola bilməz/");
            RuleFor(x => x.Password).NotNull().MinimumLength(5).WithMessage("Şifrənin uzunluğu minimum 5 simvol olmalıdır");
        }
    }
}