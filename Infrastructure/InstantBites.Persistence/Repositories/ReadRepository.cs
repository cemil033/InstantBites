using InstantBites.Application.Repositories;
using InstantBites.Domain.Entites.Common;
using InstantBites.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : class,BaseEntity
    {
        private readonly AppDBContext context;

        public ReadRepository(AppDBContext context)
        {
            this.context = context;
        }

        public DbSet<T> Table => context.Set<T>();

        public async Task<T> Get(string id, bool tracking = false)        
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> Get(Expression<Func<T, bool>> method, bool tracking = false)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
        }

        public IQueryable<T> GetAll(bool tracking = false) {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = false)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query.Where(method);
        }
    }
}
