using System;
using System.Collections.Generic;

namespace EleterosEB.Data.dbModels
{
    public partial class Doctors
    {
        public Doctors()
        {
            Appointments = new HashSet<Appointments>();
            SurgeryRoomAppointment = new HashSet<SurgeryRoomAppointment>();
        }

        public int DoctorId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Appointments> Appointments { get; set; }
        public virtual ICollection<SurgeryRoomAppointment> SurgeryRoomAppointment { get; set; }
    }
}
