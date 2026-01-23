using Jobby.Application.Exceptions;
using Jobby.Application.Features.Commands.Applicant.DTOs;
using Jobby.Application.Interfaces.Storage;
using Jobby.Application.Repositories.Applicant;
using Jobby.Application.Repositories.Vacancy;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobby.Application.Features.Commands.Applicant.Create
{
    public class CreateApplicantCommandHandler : IRequestHandler<CreateApplicantCommand, ApplicantCreateDto>
    {
        private readonly IApplicantWriteRepository _writeRepository;
        private readonly IApplicantReadRepository _readRepository;
        private readonly IVacancyReadRepository _vacancyReadRepository;
        private readonly ILocalStorage _storageService;

        public CreateApplicantCommandHandler(
            IApplicantWriteRepository writeRepository,
            IApplicantReadRepository readRepository,
            IVacancyReadRepository vacancyReadRepository,
            ILocalStorage storageService)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _vacancyReadRepository = vacancyReadRepository;
            _storageService = storageService;
        }

        public async Task<ApplicantCreateDto> Handle( CreateApplicantCommand request, CancellationToken cancellationToken)
        {
            var vacancy = await _vacancyReadRepository
                .GetByIdAsync(request.VacancyId, q => q.Include(v => v.Questions));

            if (vacancy == null || !vacancy.IsActive)
                throw new BadRequestException("Vacansiya tapılmadı");

            bool isApplied = await _readRepository
                .GetWhere(a => a.VacancyId == request.VacancyId && a.Email == request.Email).AnyAsync(cancellationToken);

            if (isApplied)
                throw new BadRequestException("Siz artıq bu vakansiya üçün müraciət etmisiniz");

            string filePath = await _storageService.Upload("applicants", request.CvFile);

            var applicant = new Domain.Entities.ApplicantAggregate.Applicant(
                request.VacancyId,
                 request.FirstName,
                 request.LastName,
                 request.Email,
                 request.Phone
            );

            applicant.SetCvPath(filePath);

            await _writeRepository.AddAsync(applicant);
            await _writeRepository.SaveAsync();

            return new ApplicantCreateDto
            {
                Id = applicant.Id,
                TotalQuestions = vacancy.Questions?.Count ?? 0
            };
        }
    }

}
