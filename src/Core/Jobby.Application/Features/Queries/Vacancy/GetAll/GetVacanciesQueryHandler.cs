using AutoMapper;
using Jobby.Application.Constants;
using Jobby.Application.Features.Queries.Category.DTOs;
using Jobby.Application.Features.Queries.Vacancy.DTOs;
using Jobby.Application.Repositories.Vacancy;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobby.Application.Features.Queries.Vacancy.GetAll
{
    public class GetVacanciesQueryHandler : IRequestHandler<GetVacanciesQuery, AllDto<VacancyDto>>
    {
        private readonly IMapper _mapper;
        private readonly IVacancyReadRepository _readRepository;

        public GetVacanciesQueryHandler(IMapper mapper, IVacancyReadRepository readRepository)
        {
            _mapper = mapper;
            _readRepository = readRepository;
        }
        public async Task<AllDto<VacancyDto>> Handle(GetVacanciesQuery request, CancellationToken cancellationToken)
        {
            var vacancies = _readRepository.GetWhere(l => !l.IsDeleted);

            vacancies = vacancies.ApplySortingAndFiltering(request.ColumnName, request.OrderBy, request.FilterValue);

            int totalCount = await vacancies.CountAsync(cancellationToken);

            int totalPage = request.PageSize != null
                ? (int)Math.Ceiling(totalCount / (double)request.PageSize)
                : 1;

            if (request.PageNumber != null && request.PageSize != null)
            {
                vacancies = vacancies
                    .Skip((request.PageNumber.Value - 1) * request.PageSize.Value)
                    .Take(request.PageSize.Value);
            }

            List<VacancyDto> vacanciesDto = _mapper.Map<List<VacancyDto>>(await vacancies.ToListAsync(cancellationToken));

            return new AllDto<VacancyDto>
            {
                Data = vacanciesDto,
                TotalCount = totalCount,
                TotalPage = totalPage,
            };
        }
    }
}
