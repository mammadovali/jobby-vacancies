using Jobby.Application.Repositories.Category;
using Jobby.Persistence.Context;

namespace Jobby.Persistence.Repositories.Category
{
    public class CategoryWriteRepository : WriteRepository<Domain.Entities.CategoryAggregate.Category>, ICategoryWriteRepository
    {
        public CategoryWriteRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}