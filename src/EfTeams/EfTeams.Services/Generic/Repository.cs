using EfTeams.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EfTeams.Repositories.Generic
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        protected readonly TeamDbContext context;

        public Repository(TeamDbContext context)
        {
            this.context = context;
        }

        public async Task Add(T entity)
            => context.Set<T>().Add(entity);

        public async Task AddRange(IEnumerable<T> entity)
            => context.Set<T>().AddRange(entity);

        public virtual async Task Delete(T entity)
            => context.Set<T>().Remove(entity);

        public async Task DeleteRange(IEnumerable<T> entity)
            =>  context.Set<T>().RemoveRange(entity);

        public async Task<T> Find(Expression<Func<T, bool>> predicate)
            => await context.Set<T>().FirstOrDefaultAsync(predicate).ConfigureAwait(true);

        public async Task<T> Get(int id)
            => await context.Set<T>().FindAsync(id);

        public async Task<IEnumerable<T>> GetAll()
            => await context.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
            => await context.Set<T>().Where(predicate).ToListAsync();

        public async Task Update(T entity)
            => context.Set<T>().Update(entity);

        public async Task UpdateRange(IEnumerable<T> entity)
            => context.Set<T>().UpdateRange(entity);

    }
}
