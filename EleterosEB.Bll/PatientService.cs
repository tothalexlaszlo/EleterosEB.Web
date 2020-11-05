using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EleterosEB.Data;
using EleterosEB.Domain;

namespace EleterosEB.Bll
{
    public class PatientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> CreatePatient(Patient newPatient)
        {
            _unitOfWork.PatientRepository.Add(newPatient);
            return _unitOfWork.CommitAsync();

        }

        public Task<bool> DeletePatient(Patient patient)
        {
            _unitOfWork.PatientRepository.Delete(patient);
            return _unitOfWork.CommitAsync();
        }

        public Task<bool> UpdatePatient(Patient patient)
        {
            _unitOfWork.PatientRepository.Update(patient);
            return _unitOfWork.CommitAsync();
        }

        public Task<IReadOnlyList<Patient>> GetAllPatientsAsync()
        {
            return _unitOfWork.PatientRepository.ListAsync();
        }

        public async Task<Patient> GetPatientByNameAsync(string name)
        {
            var query = await _unitOfWork.PatientRepository.ListAsync();

            return query.FirstOrDefault(c => c.Name == name);
        }

        public Task<Patient> GetPatientsByIdAsync(int id)
        {
            return _unitOfWork.PatientRepository.GetByIdAsync(id);
        }
    }
}
