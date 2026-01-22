using Jobby.Application.Features.Commands.Applicant.DTOs;
using MediatR;

namespace Jobby.Application.Features.Commands.Applicant.SubmitAnswer
{
    public class SubmitAnswerCommand : IRequest<SubmitAnswerResultDto>
    {
        public int ApplicantId { get; set; }
        public int QuestionId { get; set; }
        public int? QuestionOptionId { get; set; }
    }
}
