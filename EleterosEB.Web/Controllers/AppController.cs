using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EleterosEB.Web.Controllers
{
    public class AppController: Controller
    {

        public AppController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Clients()
        {
            return View();
        }

        public IActionResult Doctors()
        {
            return View();
        }

        public IActionResult Rooms()
        {
            return View();
        }

        public IActionResult Patients()
        {
            return View();
        }

        public IActionResult AppointmentTypes()
        {
            return View();
        }
        public IActionResult Categories()
        {
            return View();
        }

        public IActionResult Products()
        {
            return View();
        }

        public IActionResult SurgeryRoomAppointment()
        {
            return View();
        }
    }
}
