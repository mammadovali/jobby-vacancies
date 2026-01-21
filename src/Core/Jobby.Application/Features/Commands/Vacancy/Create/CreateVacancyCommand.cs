using Jobby.Application.Constants;
using MediatR;

namespace Jobby.Application.Features.Commands.Vacancy.Create
{
    public class CreateVacancyCommand : IRequest<ResponseDto>
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
