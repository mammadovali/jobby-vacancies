using Jobby.Domain.Entities.Common;
using System.Linq.Expressions;

namespace Jobby.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : Entity
    {
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, Func<IQueryable<T>, IQueryable<T>> include = null, bool tracking = true);
        IQueryable<T> GetWhereSimple(Expression<Func<T, bool>> predicate, bool tracking = false);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, Func<IQueryable<T>, IQueryable<T>> include = null, bool tracking = true);
        Task<T> GetByIdAsync(int id, Func<IQueryable<T>, IQueryable<T>> include = null, bool tracking = true);
    }
}