using EleterosEB.Data.Repositories.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EleterosEB.Domain;

namespace EleterosEB.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EleterosEBContext _eleterosEbContext;
        public IAppointmentRepository AppointmentRepository  { get; }
        public IAppointmentTypeRepository AppointmentTypeRepository { get; }
        public IClientRepository ClientRepository { get; }
        public IDoctorRepository DoctorRepository { get; }
        public IPatientRepository PatientRepository { get; }
        public IRoomRepository RoomRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }
        public ISurgeryRoomAppointmentRepository SurgeryRoomBooking { get; }
        public UnitOfWork(IRoomRepository roomRepository,
            IDoctorRepository doctorRepository,
            IAppointmentTypeRepository appointmentTypeRepository,
            IClientRepository clientRepository,
            IPatientRepository patientRepository, 
            ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            ISurgeryRoomAppointmentRepository surgeryRoomBooking,
            EleterosEBContext eleterosEbContext, IAppointmentRepository appointmentRepository)
        {
            _eleterosEbContext = eleterosEbContext;
            AppointmentRepository = appointmentRepository;
            RoomRepository = roomRepository;
            DoctorRepository = doctorRepository;
            AppointmentTypeRepository = appointmentTypeRepository;
            ClientRepository = clientRepository;
            PatientRepository = patientRepository;
            CategoryRepository = categoryRepository;
            ProductRepository = productRepository;
            SurgeryRoomBooking = surgeryRoomBooking;
        }

        public async Task<bool> CommitAsync()
        {
            return (await _eleterosEbContext.SaveChangesAsync()) > 0;
        }
    }
}