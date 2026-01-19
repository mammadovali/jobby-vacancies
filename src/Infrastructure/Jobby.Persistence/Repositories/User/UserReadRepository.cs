using Jobby.Application.Repositories.User;
using Jobby.Persistence.Context;

namespace Jobby.Persistence.Repositories.User
{
    public class UserReadRepository : ReadRepository<Domain.Entities.Identity.User>, IUserReadRepository
    {
        public UserReadRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
