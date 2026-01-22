using Jobby.Application.Features.Commands.Applicant.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Jobby.Application.Features.Commands.Applicant.Create
{
    public class CreateApplicantCommand : IRequest<ApplicantCreateDto>
    {
        public int VacancyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public IFormFile CvFile { get; set; }
    }
}
