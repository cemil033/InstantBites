using InstantBites.Domain.Entites.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : class ,BaseEntity
    {
        Task<bool> AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        Task<bool> Update(T entity);
        bool Remove(T entity);
        Task<bool> RemoveAsync(string id);
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}
