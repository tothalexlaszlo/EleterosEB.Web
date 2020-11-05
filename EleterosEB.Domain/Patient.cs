using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleterosEB.Domain
{
    public class Patient
    {
        public int PatientId { get; set; }
        public int? ClientId { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public string AnimalTypeSpecies { get; set; }
        public string AnimalTypeBreed { get; set; }

        public virtual Client Client { get; set; }

    }
}
