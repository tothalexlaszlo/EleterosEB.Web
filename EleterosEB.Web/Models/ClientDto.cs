using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace EleterosEB.Web.Models
{
    public class ClientDto
    {
        public int ClientId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public ICollection<PatientDto> Patients { get; set; }
    }
}