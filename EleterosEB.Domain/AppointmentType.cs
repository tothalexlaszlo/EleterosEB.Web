using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleterosEB.Domain
{
    public class AppointmentType
    {
        //public AppointmentType()
        //{
        //    Appointments = new HashSet<Appointment>();
        //}

        public int AppointmentTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Duration { get; set; }

        //public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
