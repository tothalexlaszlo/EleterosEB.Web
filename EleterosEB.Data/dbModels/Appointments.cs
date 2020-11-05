using System;
using System.Collections.Generic;

namespace EleterosEB.Data.dbModels
{
    public partial class Appointments
    {
        public int AppointmentId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? ClientId { get; set; }
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }
        public int? AppointmentTypeId { get; set; }
        public bool IsPotentiallyConflicting { get; set; }

        public virtual AppointmentTypes AppointmentType { get; set; }
        public virtual Clients Client { get; set; }
        public virtual Doctors Doctor { get; set; }
        public virtual Patients Patient { get; set; }
    }
}
