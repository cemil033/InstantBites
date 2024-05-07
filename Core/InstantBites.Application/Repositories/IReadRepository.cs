using InstantBites.Domain.Entites.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : class,BaseEntity
    {
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> Get(string id, bool tracking = true);
        Task<T> Get(Expression<Func<T, bool>> method, bool tracking = true);

    }
}
