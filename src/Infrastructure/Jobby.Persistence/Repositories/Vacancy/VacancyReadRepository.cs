using Jobby.Application.Repositories.Vacancy;
using Jobby.Persistence.Context;

namespace Jobby.Persistence.Repositories.Vacancy
{
    public class VacancyReadRepository : ReadRepository<Domain.Entities.VacancyAggragate.Vacancy>, IVacancyReadRepository
    {
        public VacancyReadRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
