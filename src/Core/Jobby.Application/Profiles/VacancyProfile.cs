using AutoMapper;
using Jobby.Application.Features.Queries.Vacancy.DTOs;

namespace Jobby.Application.Profiles
{
    public class VacancyProfile : Profile
    {
        public VacancyProfile()
        {
            CreateMap<Domain.Entities.VacancyAggragate.Vacancy, VacancyDto>();
        }
    }
}