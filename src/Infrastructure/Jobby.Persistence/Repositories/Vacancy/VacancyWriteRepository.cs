using Jobby.Application.Repositories.Vacancy;
using Jobby.Persistence.Context;

namespace Jobby.Persistence.Repositories.Vacancy
{
    public class VacancyWriteRepository : WriteRepository<Domain.Entities.VacancyAggragate.Vacancy>, IVacancyWriteRepository
    {
        public VacancyWriteRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}