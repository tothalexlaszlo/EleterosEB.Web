using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleterosEB.Web.Models
{
    public class AppointmentDto
    {
        //public int AppointmentId { get; set; }
        public int? ClientId { get; set; }
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }
        public int? AppointmentTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Title { get; set; }
        public bool IsPotentiallyConflicting { get; set; }
        //public AppointmentTypeDto AppointmentType { get; set; }


    }
}



