using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleterosEB.Web.Models
{
    public class RoomDto
    { 
        [Required]
        public string Name { get; set; }
    }
}
