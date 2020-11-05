using System;
using System.Collections.Generic;

namespace EleterosEB.Data.dbModels
{
    public partial class Patients
    {
        public Patients()
        {
            Appointments = new HashSet<Appointments>();
        }

        public int PatientId { get; set; }
        public int? ClientId { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public string AnimalTypeSpecies { get; set; }
        public string AnimalTypeBreed { get; set; }

        public virtual Clients Client { get; set; }
        public virtual ICollection<Appointments> Appointments { get; set; }
    }
}
