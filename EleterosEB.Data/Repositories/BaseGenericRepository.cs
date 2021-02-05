using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EleterosEB.Data.Repositories.Intefaces;
using EleterosEB.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EleterosEB.Data.Repositories
{
    public abstract class BaseGenericRepository<T>
        : IRepository<T> where T : class
    {
        private readonly EleterosEBContext _context;

        protected BaseGenericRepository(EleterosEBContext context)
        {
            _context = context;
        }

        public virtual void Add(T entity)
        {
            //_logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
        }

        public virtual void Update(T entity)
        {
            //_logger.LogInformation($"Updating an object of type {entity.GetType()} to the context.");

            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _context.Set<T>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                _context.Entry(entity).CurrentValues.SetValues(entity);
            }
        }

        public virtual void Delete(T entity)
        {
            //_logger.LogInformation($"Removing an object of type {entity.GetType()} from the context.");
            _context.Remove(entity);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
           // _logger.LogInformation($"Getting an object asynchronous of type {typeof(T)}" +
            //                       $"with Id: {id}");

            return await _context.FindAsync<T>(id);
        }

        //public virtual async Task<IReadOnlyList<T>> ListAsync()
        //{
        //    //_logger.LogInformation($"Getting all {typeof(T)} asynchronous");
        //    //wait _context.Database.ExecuteSqlRawAsync("WAITFOR DELAY '00:00:02';");

        //    return await _context.Set<T>().ToListAsync();
        //}

        public virtual async Task<IReadOnlyList<T>> ListAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> result = _context.Set<T>();
            foreach (var include in includes)
            {
                result = result.Include(include);
            }

            return await result.ToListAsync();
        }

    }
}
