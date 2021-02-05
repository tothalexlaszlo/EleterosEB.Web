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
    public class ClientRepository: BaseGenericRepository<Client>, IClientRepository
    {
        public ClientRepository(EleterosEBContext context) 
            : base(context)
        {
        }
    }
}
