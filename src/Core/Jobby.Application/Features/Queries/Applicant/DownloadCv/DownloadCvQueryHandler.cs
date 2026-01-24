using Jobby.Application.Constants;
using Jobby.Application.Exceptions;
using Jobby.Application.Repositories.Applicant;
using MediatR;

namespace Jobby.Application.Features.Queries.Applicant.DownloadCv
{
    public class DownloadCvQueryHandler : IRequestHandler<DownloadCvQuery, FileDto>
    {
        private readonly IApplicantReadRepository _readRepository;

        public DownloadCvQueryHandler(IApplicantReadRepository readRepository)
        {
            _readRepository = readRepository;
        }
        public async Task<FileDto> Handle(DownloadCvQuery request, CancellationToken cancellationToken)
        {
            var applicant = await _readRepository.GetByIdAsync(request.Id);

            if (applicant is null)
                throw new BadRequestException("Namizəd tapılmadı");

            if (string.IsNullOrWhiteSpace(applicant.CvFilePath))
                throw new BadRequestException("Bu imtahan üçün fayl əlavə olunmayıb");

            string filePath = Path.Combine("App_Data", applicant.CvFilePath);

            if (!File.Exists(filePath))
                throw new NotFoundException("Fayl tapılmadı");

            byte[] fileBytes = await File.ReadAllBytesAsync(filePath);
            string fileName = Path.GetFileName(filePath);
            string contentType = "application/pdf";

            return new FileDto
            {
                FileBytes = fileBytes,
                FileName = fileName,
                ContentType = contentType
            };
        }
    }
}
