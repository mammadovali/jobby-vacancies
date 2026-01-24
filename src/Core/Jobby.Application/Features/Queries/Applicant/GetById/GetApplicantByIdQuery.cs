using Jobby.Application.Features.Queries.Applicant.DTOs;
using MediatR;

namespace Jobby.Application.Features.Queries.Applicant.GetById
{
    public class GetApplicantByIdQuery : IRequest<ApplicantDetailDto>
    {
        public int Id { get; set; }
        public GetApplicantByIdQuery(int id)
        {
            Id = id;
        }
    }
}