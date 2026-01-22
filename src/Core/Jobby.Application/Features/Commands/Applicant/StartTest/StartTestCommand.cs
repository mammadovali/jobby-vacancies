using Jobby.Application.Features.Commands.Applicant.DTOs;
using Jobby.Application.Features.Queries.Question.DTOs;
using MediatR;

namespace Jobby.Application.Features.Commands.Applicant.StartTest
{
    public class StartTestCommand : IRequest<QuestionApplicantDto>
    {
        public int ApplicantId { get; set; }
    }
}
