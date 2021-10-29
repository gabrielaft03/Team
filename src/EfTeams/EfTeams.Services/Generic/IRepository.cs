using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EfTeams.Repositories.Generic
{
    public interface IRepository<T> where T : class, new()
    {
        public Task<T> Get(int id);
        public Task<T> Find(Expression<Func<T,bool>>predicate);
        public Task<IEnumerable<T>> GetAll();
        public Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);
        public Task Add(T entity);
        public Task AddRange(IEnumerable<T> entity);
        public Task Update(T entity);
        public Task UpdateRange(IEnumerable<T> entity); 
        public Task Delete(T entity);
        public Task DeleteRange(IEnumerable<T> entity);
    }
}
