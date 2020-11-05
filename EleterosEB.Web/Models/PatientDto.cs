using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleterosEB.Web.Models
{
    public class PatientDto
    {
        [Required]
        public int PatientId { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
        public int Gender { get; set; }
        public string AnimalTypeSpecies { get; set; }
        public string AnimalTypeBreed { get; set; }
    }
}
