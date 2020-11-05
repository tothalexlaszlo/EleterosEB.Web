using System;
using System.Collections.Generic;

namespace EleterosEB.Data.dbModels
{
    public partial class Clients
    {
        public Clients()
        {
            Appointments = new HashSet<Appointments>();
            Patients = new HashSet<Patients>();
        }

        public int ClientId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public virtual ICollection<Appointments> Appointments { get; set; }
        public virtual ICollection<Patients> Patients { get; set; }
    }
}
