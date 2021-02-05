using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleterosEB.Web.Models
{
    public class AppointmentTypeDto
    {
        public int? AppointmentTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Code { get; set; }
        public int Duration { get; set; }
    }
}
