namespace Jobby.Application.Features.Commands.Applicant.DTOs
{
    public class SubmitAnswerResultDto
    {
        public bool IsFinished { get; set; }
        //public int? NextQuestionId { get; set; }

        public QuestionApplicantDto NextQuestion { get; set; }
    }
}
