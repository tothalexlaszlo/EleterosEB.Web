using System;
using System.Collections.Generic;

namespace EleterosEB.Data.dbModels
{
    public partial class AppointmentTypes
    {
        public AppointmentTypes()
        {
            Appointments = new HashSet<Appointments>();
        }

        public int AppointmentTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Duration { get; set; }

        public virtual ICollection<Appointments> Appointments { get; set; }
    }
}
