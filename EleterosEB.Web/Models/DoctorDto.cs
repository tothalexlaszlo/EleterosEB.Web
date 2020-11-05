using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleterosEB.Web.Models
{
    public class DoctorDto
    {
        [Required]
        public string Name { get; set; }
    }
}
