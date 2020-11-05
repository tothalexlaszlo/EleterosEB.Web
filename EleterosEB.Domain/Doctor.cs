using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleterosEB.Domain
{
    public class Doctor
    {
        public Doctor()
        {
            //Appointments = new HashSet<Appointment>();
            //Patients = new HashSet<Patient>();
        }

        public int DoctorId { get; set; }
        public string Name { get; set; }

        //public virtual ICollection<Appointment> Appointments { get; set; }
        //public virtual ICollection<Patient> Patients { get; set; }
    }
}
