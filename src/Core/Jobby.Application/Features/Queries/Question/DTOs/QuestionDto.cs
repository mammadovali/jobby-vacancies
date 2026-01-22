namespace Jobby.Application.Features.Queries.Question.DTOs
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }
        public List<QuestionOptionResponseDto> Options { get; set; } = new();
    }
}
