using Jobby.Application.Repositories.Category;
using Jobby.Persistence.Context;

namespace Jobby.Persistence.Repositories.Category
{
    public class CategoryReadRepository : ReadRepository<Domain.Entities.CategoryAggregate.Category>, ICategoryReadRepository
    {
        public CategoryReadRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
