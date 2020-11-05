using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EleterosEB.Data;
using EleterosEB.Domain;

namespace EleterosEB.Bll
{
    public class AppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> CreateAppointment(Appointment newAppointment)
        {
            _unitOfWork.AppointmentRepository.Add(newAppointment);
            return _unitOfWork.CommitAsync();

        }

        public Task<bool> DeleteAppointment(Appointment appointment)
        {
            _unitOfWork.AppointmentRepository.Delete(appointment);
            return _unitOfWork.CommitAsync();
        }

        public Task<bool> UpdateAppointment(Appointment appointment)
        {
            appointment.StartDate = appointment.StartDate.AddHours(1);
            appointment.EndDate = appointment.EndDate.AddHours(1);

            _unitOfWork.AppointmentRepository.Update(appointment);
            return _unitOfWork.CommitAsync();
        }

        public async Task<IReadOnlyList<Appointment>> GetAllAppointmentsAsync()
        {
            return await _unitOfWork.AppointmentRepository.ListAsync(a => a.AppointmentType );
        }

        public Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            return _unitOfWork.AppointmentRepository.GetByIdAsync(id);
        }
    }
}
