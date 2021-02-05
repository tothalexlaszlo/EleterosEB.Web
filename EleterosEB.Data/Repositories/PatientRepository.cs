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
    public class PatientRepository: BaseGenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(EleterosEBContext context)
            : base(context)
        {
        }

    }
}
