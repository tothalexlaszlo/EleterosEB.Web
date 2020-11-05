using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleterosEB.Domain
{
    public class Room
    {
        //public Room()
        //{
        //    Appointments = new HashSet<Appointment>();
        //}

        public int RoomId { get; set; }
        public string Name { get; set; }

        //public virtual ICollection<Appointment> Appointments { get; set; }

    }
}
