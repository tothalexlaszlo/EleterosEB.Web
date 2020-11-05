using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace EleterosEB.Domain
{
    public class Client
    {
        public Client()
        {
            //Appointments = new HashSet<Appointment>();
            Patients = new HashSet<Patient>();
        }

        public int ClientId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        //public Address Address { get; set; }

        //public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }

    }
}
