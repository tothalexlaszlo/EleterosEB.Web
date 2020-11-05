using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleterosEB.Web.Models
{
    public class SurgeryRoomAppointmentDto
    {
        //public int SurgeryRoomBookingId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DoctorId { get; set; }
        public int RoomId { get; set; }

        public bool IsConflicting { get; set; }
    }
}
