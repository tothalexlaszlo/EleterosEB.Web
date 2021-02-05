using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EleterosEB.Domain;
using EleterosEB.Web.Models;

namespace EleterosEB.Web
{
    public class AutoMapperConfig: Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Room, RoomDto>()
                .ReverseMap();

            CreateMap<Doctor, DoctorDto>()
                .ReverseMap();

            CreateMap<AppointmentType, AppointmentTypeDto>()
                .ReverseMap();

            CreateMap<Client, ClientDto>()
                .ReverseMap();

            CreateMap<Patient, PatientDto>()
                .ReverseMap();

            CreateMap<Appointment, AppointmentDto>()
                .ReverseMap();

            CreateMap<Product, ProductDto>()
                .ReverseMap();

            CreateMap<Category, CategoryDto>()
                .ReverseMap();


            CreateMap<SurgeryRoomAppointment, SurgeryRoomAppointmentDto>()
                .ReverseMap();
        }
    }
}
