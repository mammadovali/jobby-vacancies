using Jobby.Application.Repositories.User;
using Jobby.Persistence.Context;

namespace Jobby.Persistence.Repositories.User
{
    public class UserWriteRepository : WriteRepository<Domain.Entities.Identity.User>, IUserWriteRepository
    {
        public UserWriteRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
