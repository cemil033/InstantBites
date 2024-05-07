using InstantBites.Application.Repositories;
using InstantBites.Domain.Entites.Common;
using InstantBites.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class,BaseEntity
    {
        private readonly AppDBContext context;

        public WriteRepository(AppDBContext context)
        {
            this.context = context;
        }

        public DbSet<T> Table => context.Set<T>();

        public async Task<bool> AddAsync(T entity)
        {
            var entry = await Table.AddAsync(entity);
            return entry.State == EntityState.Added;
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public bool Remove(T entity)
        {
            var entry = Table.Remove(entity);
            return entry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            T model = await Table.FindAsync(id);
            var entry = Table.Remove(model);
            return entry.State == EntityState.Deleted;
        }
        public async Task<int> SaveChangesAsync()
            => await context.SaveChangesAsync();
        public int SaveChanges()
        { 
            return context.SaveChanges(true); 
        }
        public async Task<bool> Update(T entity)
        {
            var entry = Table.Update(entity);
            return entry.State == EntityState.Modified;
        }
    }
}
