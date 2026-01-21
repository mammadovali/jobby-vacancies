using Jobby.Application.Constants;
using MediatR;

namespace Jobby.Application.Features.Commands.Vacancy.Update;

public class UpdateVacancyCommand : IRequest<ResponseDto>
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}