using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EleterosEB.Data;
using EleterosEB.Domain;

namespace EleterosEB.Bll
{
    public class SurgeryRoomAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SurgeryRoomAppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> CreateSurgeryRoomAppointment(SurgeryRoomAppointment newSurgeryRoomAppointment)
        {
            _unitOfWork.SurgeryRoomBooking.Add(newSurgeryRoomAppointment);
            return _unitOfWork.CommitAsync();

        }

        public Task<bool> DeleteSurgeryRoomAppointment(SurgeryRoomAppointment surgeryRoomAppointment)
        {
            _unitOfWork.SurgeryRoomBooking.Delete(surgeryRoomAppointment);
            return _unitOfWork.CommitAsync();
        }

        public Task<bool> UpdateSurgeryRoomAppointment(SurgeryRoomAppointment surgeryRoomAppointment)
        {
            _unitOfWork.SurgeryRoomBooking.Update(surgeryRoomAppointment);
            return _unitOfWork.CommitAsync();
        }

        public Task<IReadOnlyList<SurgeryRoomAppointment>> GetAllSurgeryRoomAppointmentsAsync()
        {
            return _unitOfWork.SurgeryRoomBooking.ListAsync();
        }

        public async Task<SurgeryRoomAppointment> GetSurgeryRoomAppointmentByTitleAsync(string title)
        {
            var query = await _unitOfWork.SurgeryRoomBooking.ListAsync();

            return query.FirstOrDefault(r => r.Title == title);
        }

        public Task<SurgeryRoomAppointment> GetSurgeryRoomAppointmentByIdAsync(int id)
        {
            return _unitOfWork.SurgeryRoomBooking.GetByIdAsync(id);
        }
    }
}
