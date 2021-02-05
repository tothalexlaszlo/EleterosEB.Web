using EleterosEB.Data;
using EleterosEB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EleterosEB.Bll
{
    public class AppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateAppointment(Appointment newAppointment)
        {
            var appointmentsWithSpecificStartingDay = await GetAllAppointmentWithSpecificStartingDay(newAppointment.StartDate);
            newAppointment.IsPotentiallyConflicting = false;

            foreach (var appointment in appointmentsWithSpecificStartingDay)
            {
                if (DateIsConflicting(newAppointment.StartDate, appointment))
                {
                    newAppointment.IsPotentiallyConflicting = true;
                }
            }


            _unitOfWork.AppointmentRepository.Add(newAppointment);
            return await _unitOfWork.CommitAsync();

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

        private bool DateIsConflicting(DateTime targetDateTime, Appointment appointment)
        {
            return targetDateTime.Ticks > appointment.EndDate.Ticks && targetDateTime.Ticks < appointment.StartDate.Ticks;
        }

        private async Task<IEnumerable<Appointment>> GetAllAppointmentWithSpecificStartingDay(DateTime startDateTime)
        {
            var appointments = await _unitOfWork.AppointmentRepository.ListAsync();
            return appointments.Where(appt => appt.StartDate.Day == startDateTime.Day);
        }
    }
}
