using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EleterosEB.Data.Repositories.Intefaces;
using EleterosEB.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EleterosEB.Data.Repositories
{
    public class RoomRepository: BaseGenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(EleterosEBContext context)
            : base(context)
        {
        }

        //private readonly EleterosEBContext _eleterosEbContext;
        //private readonly ILogger<RoomRepository> _logger;

        //public RoomRepository(EleterosEBContext eleterosEbContext, ILogger<RoomRepository> logger)
        //{
        //    _eleterosEbContext = eleterosEbContext;
        //    _logger = logger;
        //}

        //public void Add(Room entity)
        //{
        //    _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
        //    _eleterosEbContext.Add(entity);
        //}

        //public void Delete(Room entity)
        //{
        //    _logger.LogInformation($"Removing an object of type {entity.GetType()} from the context.");
        //    _eleterosEbContext.Remove(entity);
        //}

        //public async Task<Room> Get(int id)
        //{
        //    return await _eleterosEbContext.Rooms.FindAsync(id);
        //}

        //public IQueryable<Room> List(params Expression<Func<Room, object>>[] includes)
        //{

        //    _logger.LogInformation($"Getting all Rooms");

        //    IQueryable<Room> rooms = _context.Rooms;
        //    foreach (var include in includes)
        //    {
        //        rooms = rooms.Include(include);
        //    }

        //    return rooms;
        //}

        //public void Update(Room entity)
        //{
        //    _logger.LogInformation($"Updating an object of type {entity.GetType()} to the context.");

        //    var entry = _eleterosEbContext.Entry(entity);
        //    if (entry.State == EntityState.Detached)
        //    {
        //        _eleterosEbContext.Rooms.Attach(entity);
        //        _eleterosEbContext.Entry(entity).State = EntityState.Modified;
        //    }
        //    else
        //    {
        //        _eleterosEbContext.Entry(entity).CurrentValues.SetValues(entity);
        //    }
        //}

        //public async Task<IReadOnlyList<Room>> ListAsync()
        //{
        //    return await _eleterosEbContext.Rooms.ToListAsync();
        //}


    }
}
