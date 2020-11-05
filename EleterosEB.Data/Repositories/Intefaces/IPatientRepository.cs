using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EleterosEB.Domain;

namespace EleterosEB.Data.Repositories.Intefaces
{
    public interface IPatientRepository: IRepository<Patient>
    {
        //void Add(Patient p);
        //IQueryable<Patient> List(); //read
        //Patient Get(int patientId); //read
        //void Update(Patient p);
        //// void Update<T> (int productId, T p);
        //// void Delete(Category p);
        //void Delete(int patientId);
    }
}
