using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EleterosEB.Data;
using EleterosEB.Domain;
using Microsoft.EntityFrameworkCore;

namespace EleterosEB.Bll
{
    public class DoctorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> CreateDoctor(Doctor newDoctor)
        {
            _unitOfWork.DoctorRepository.Add(newDoctor);
            return _unitOfWork.CommitAsync();

        }

        public Task<bool> DeleteDoctor(Doctor doctor)
        {
            _unitOfWork.DoctorRepository.Delete(doctor);
            return _unitOfWork.CommitAsync();
        }

        public Task<bool> UpdateDoctor(Doctor doctor)
        {
            _unitOfWork.DoctorRepository.Update(doctor);
            return _unitOfWork.CommitAsync();
        }

        public async Task<IReadOnlyList<Doctor>> GetAllDoctorsAsync()
        {
            return await _unitOfWork.DoctorRepository.ListAsync();
        }


        public Task<Doctor> GetDoctorByIdAsync(int id)
        {
            return _unitOfWork.DoctorRepository.GetByIdAsync(id);
        }

        public async Task<Doctor> GetDoctorByNameAsync(string name)
        {
            IReadOnlyList<Doctor> query = await _unitOfWork.DoctorRepository.ListAsync();

            return query.FirstOrDefault(d => d.Name == name);
        }
    }
}
