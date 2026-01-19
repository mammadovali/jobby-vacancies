using Jobby.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Jobby.Application.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        DbSet<T> Table { get; }
    }
}
