using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EleterosEB.Data.Repositories.Intefaces;
using EleterosEB.Domain;
using Microsoft.Extensions.Logging;

namespace EleterosEB.Data.Repositories
{
    public class ProductRepository: BaseGenericRepository<Product>, IProductRepository
    {
        public ProductRepository(EleterosEBContext context, ILogger<BaseGenericRepository<Product>> logger) : base(context, logger)
        {
        }
    }
}
