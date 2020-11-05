using System;
using System.Threading.Tasks;
using EleterosEB.Data.Repositories.Intefaces;
using EleterosEB.Domain;

namespace EleterosEB.Data
{
    public interface IUnitOfWork
    {
        IAppointmentRepository AppointmentRepository { get; }
        IAppointmentTypeRepository AppointmentTypeRepository { get; }
        IClientRepository ClientRepository { get; }
        IDoctorRepository DoctorRepository { get; }
        IPatientRepository PatientRepository { get; }
        IRoomRepository RoomRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        ISurgeryRoomAppointmentRepository SurgeryRoomBooking { get; }
        Task<bool> CommitAsync();
    }
}
