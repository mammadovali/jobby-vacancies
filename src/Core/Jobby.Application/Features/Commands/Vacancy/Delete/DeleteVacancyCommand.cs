using Jobby.Application.Constants;
using MediatR;

namespace Jobby.Application.Features.Commands.Vacancy.Delete
{
    public class DeleteVacancyCommand : IRequest<ResponseDto>
    {
        public int Id { get; set; }

        public DeleteVacancyCommand(int id)
        {
            Id = id;
        }
    }
}
