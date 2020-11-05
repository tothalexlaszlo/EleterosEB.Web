using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EleterosEB.Domain;


namespace EleterosEB.Data.Repositories.Intefaces
{
    public interface IRepository<TEntity>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<TEntity> GetByIdAsync(int id);
        Task<IReadOnlyList<TEntity>> ListAsync(params Expression<Func<TEntity, object>>[] includes);
    }
}
