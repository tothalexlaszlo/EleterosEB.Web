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
    public class CategoryRepository: BaseGenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(EleterosEBContext context) : base(context)
        {
        }
    }
}
