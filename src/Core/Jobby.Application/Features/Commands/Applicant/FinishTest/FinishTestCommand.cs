using Jobby.Application.Features.Commands.Applicant.DTOs;
using MediatR;

namespace Jobby.Application.Features.Commands.Applicant.FinishTest
{
    public class FinishTestCommand : IRequest<FinishTestResultDto>
    {
        public int ApplicantId { get; set; }
    }
}
