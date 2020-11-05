using System;

namespace EleterosEB.Domain
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? ClientId { get; set; }
        public int? DoctorId { get; set; }
        public int? AppointmentTypeId { get; set; }
        public int? PatientId { get; set; }
        public bool IsPotentiallyConflicting { get; set; }
        public virtual AppointmentType AppointmentType { get; set; }
    }
}