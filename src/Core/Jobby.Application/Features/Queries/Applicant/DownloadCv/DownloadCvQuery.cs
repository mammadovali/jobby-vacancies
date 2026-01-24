using Jobby.Application.Constants;
using MediatR;

namespace Jobby.Application.Features.Queries.Applicant.DownloadCv
{
    public class DownloadCvQuery : IRequest<FileDto>
    {
        public int Id { get; set; }
        public DownloadCvQuery(int id)
        {
            Id = id;
        }
    }
}
