using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EleterosEB.Data;
using EleterosEB.Domain;

namespace EleterosEB.Bll
{
    public class AppointmentTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> CreateAppointmentType(AppointmentType newAppointmentType)
        {
            _unitOfWork.AppointmentTypeRepository.Add(newAppointmentType);
            return _unitOfWork.CommitAsync();

        }

        public Task<bool> DeleteAppointmentType(AppointmentType appointmentType)
        {
            _unitOfWork.AppointmentTypeRepository.Delete(appointmentType);
            return _unitOfWork.CommitAsync();
        }

        public Task<bool> UpdateAppointmentType(AppointmentType appointmentType)
        {
            _unitOfWork.AppointmentTypeRepository.Update(appointmentType);
            return _unitOfWork.CommitAsync();
        }

        public async Task<IReadOnlyList<AppointmentType>> GetAllAppointmentTypesAsync()
        {
            return await _unitOfWork.AppointmentTypeRepository.ListAsync();
        }


        public Task<AppointmentType> GetAppointmentTypeByIdAsync(int id)
        {
            return _unitOfWork.AppointmentTypeRepository.GetByIdAsync(id);
        }

        public async Task<AppointmentType> GetAppointmentTypeByNameAsync(string name)
        {
            IReadOnlyList<AppointmentType> query = await _unitOfWork.AppointmentTypeRepository.ListAsync();

            return query.FirstOrDefault(d => d.Name == name);
        }

    }
}
