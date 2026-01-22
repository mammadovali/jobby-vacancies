using FluentValidation;

namespace Jobby.Application.Features.Commands.Applicant.Create
{
    public class CreateApplicantCommandValidator : AbstractValidator<CreateApplicantCommand>
    {
        public CreateApplicantCommandValidator()
        {
            RuleFor(x => x.VacancyId)
                .GreaterThan(0).WithMessage("Vakansiya ID 0 ola bilməz");

            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Ad boş ola bilməz")
                .MaximumLength(100).WithMessage("Adın uzunluğu maksimum 100 simvol ola bilər");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Soyad boş ola bilməz")
                .MaximumLength(100).WithMessage("Soyadın uzunluğu maksimum 100 simvol ola bilər");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email boş ola bilməz")
                .EmailAddress().WithMessage("Email düzgün formatda olmalıdır")
                .MaximumLength(150).WithMessage("Emailin uzunluğu maksimum 150 simvol ola bilər");

            RuleFor(x => x.Phone)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Telefon nömrəsi boş ola bilməz")
                .MaximumLength(50).WithMessage("Telefon nömrəsinin uzunluğu maksimum 50 simvol ola bilər");

            RuleFor(i => i.CvFile)
            .Must(file => file == null || IsAllowedFileExtension(file.FileName)).WithMessage("Yalnız PDF formatında fayllar qəbul edilir");
        }
        private bool IsAllowedFileExtension(string fileName)
        {
            var allowedExtensions = new[] { ".pdf" };
            var fileExtension = Path.GetExtension(fileName)?.ToLowerInvariant();
            return allowedExtensions.Contains(fileExtension);
        }
    }
}