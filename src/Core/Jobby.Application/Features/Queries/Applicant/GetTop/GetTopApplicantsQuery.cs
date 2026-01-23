using Jobby.Application.Features.Queries.Applicant.DTOs;
using MediatR;

namespace Jobby.Application.Features.Queries.Applicant.GetTop
{
    public class GetTopApplicantsQuery : IRequest<List<TopApplicantDto>>
    {
        public int TopCount { get; set; }

        public GetTopApplicantsQuery(int topCount)
        {
            TopCount = topCount;
        }
    }

}
