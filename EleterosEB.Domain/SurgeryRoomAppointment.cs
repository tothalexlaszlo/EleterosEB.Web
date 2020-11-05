using System;
using System.Collections.Generic;

namespace EleterosEB.Domain
{
    public partial class SurgeryRoomAppointment
    {
        public int SurgeryRoomAppointmentId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DoctorId { get; set; }
        public int RoomId { get; set; }

        public bool IsConflicting { get; set; }
    }
}