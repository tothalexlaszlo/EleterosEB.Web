using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EleterosEB.Bll;
using EleterosEB.Domain;
using EleterosEB.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EleterosEB.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController: ControllerBase
    {
        private readonly AppointmentService _appointmentService;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public AppointmentsController(AppointmentService appointmentService, IMapper mapper, LinkGenerator linkGenerator)
        {
            _appointmentService = appointmentService;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            
            try
            {
                var result = await _appointmentService.GetAllAppointmentsAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> Post(AppointmentDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var existing = await _appointmentService.GetAppointmentByIdAsync(model.AppointmentId);
                    //if (existing != null)
                    //{
                    //    return BadRequest("The appointment is already exists!");
                    //}

                    //var location = _linkGenerator.GetPathByAction(HttpContext,
                    //    "Get", "Appointments",
                    //    new { name = model.AppointmentId });

                    //if (string.IsNullOrWhiteSpace(location))
                    //{
                    //    return BadRequest("Could not use current id");
                    //}

                    var appointment = _mapper.Map<Appointment>(model);

                    if (await _appointmentService.CreateAppointment(appointment))
                    {
                        return StatusCode(StatusCodes.Status201Created, appointment);
                    }

                    return BadRequest("Something bad happened!");
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var oldClient = await _appointmentService.GetAppointmentByIdAsync(id);
                if (oldClient == null) return NotFound();

                if (await _appointmentService.DeleteAppointment(oldClient))
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e}");
            }

            return BadRequest("Failed to delete the appointment");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AppointmentDto>> Put(int id, AppointmentDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

                var oldAppointment = await _appointmentService.GetAppointmentByIdAsync(id);
                if (oldAppointment == null) return NotFound($"Could not find appointment with id of {id}");

                var updatedAppointment = _mapper.Map(model, oldAppointment);

                if (await _appointmentService.UpdateAppointment(updatedAppointment))
                {
                    return Ok(updatedAppointment);
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e}");
            }

            return BadRequest();
        }


    }
}

