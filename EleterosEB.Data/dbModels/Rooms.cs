using System;
using System.Collections.Generic;

namespace EleterosEB.Data.dbModels
{
    public partial class Rooms
    {
        public Rooms()
        {
            SurgeryRoomAppointment = new HashSet<SurgeryRoomAppointment>();
        }

        public int RoomId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SurgeryRoomAppointment> SurgeryRoomAppointment { get; set; }
    }
}
