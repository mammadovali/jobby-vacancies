using AutoMapper;
using Jobby.Application.Exceptions;
using Jobby.Application.Features.Queries.Vacancy.DTOs;
using Jobby.Application.Repositories.Vacancy;
using MediatR;

namespace Jobby.Application.Features.Queries.Vacancy.GetById
{
    public class GetVacancyByIdQueryHandler : IRequestHandler<GetVacancyByIdQuery, VacancyDto>
    {
        private readonly IMapper _mapper;
        private readonly IVacancyReadRepository _readRepository;

        public GetVacancyByIdQueryHandler(IMapper mapper, IVacancyReadRepository readRepository)
        {
            _mapper = mapper;
            _readRepository = readRepository;
        }
        public async Task<VacancyDto> Handle(GetVacancyByIdQuery request, CancellationToken cancellationToken)
        {
            var vacancy = await _readRepository.GetByIdAsync(request.Id);

            if (vacancy is null)
                throw new BadRequestException("Vakansiya tapılmadı");

            var vacancyDto = _mapper.Map<VacancyDto>(vacancy);

            return vacancyDto;
        }
    }
}
