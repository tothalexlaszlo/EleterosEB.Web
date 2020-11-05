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
    public class SurgeryRoomAppointmentsController: ControllerBase
    {
        private readonly SurgeryRoomAppointmentService _surgeryRoomAppointmentService;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public SurgeryRoomAppointmentsController(SurgeryRoomAppointmentService surgeryRoomAppointmentService, IMapper mapper,
            LinkGenerator linkGenerator)
        {
            _surgeryRoomAppointmentService = surgeryRoomAppointmentService;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _surgeryRoomAppointmentService.GetAllSurgeryRoomAppointmentsAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SurgeryRoomAppointment>> Get(int id)
        {
            try
            {
                var result = await _surgeryRoomAppointmentService.GetSurgeryRoomAppointmentByIdAsync(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SurgeryRoomAppointment>> Post(SurgeryRoomAppointmentDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existing = await _surgeryRoomAppointmentService.GetSurgeryRoomAppointmentByTitleAsync(model.Title);
                    if (existing != null)
                    {
                        return BadRequest("The SurgeryRoomAppointment is already exists!");
                    }

                    var location = _linkGenerator.GetPathByAction(HttpContext,
                        "Get", "SurgeryRoomAppointments",
                        new { name = model.Title });

                    if (string.IsNullOrWhiteSpace(location))
                    {
                        return BadRequest("Could not use current name");
                    }

                    var surgeryRoomAppointment = _mapper.Map<SurgeryRoomAppointment>(model);

                    if (await _surgeryRoomAppointmentService.CreateSurgeryRoomAppointment(surgeryRoomAppointment))
                    {
                        return Created(location, surgeryRoomAppointment);
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
                var oldSurgeryRoomAppointment = await _surgeryRoomAppointmentService.GetSurgeryRoomAppointmentByIdAsync(id);
                if (oldSurgeryRoomAppointment == null) return NotFound();

                if (await _surgeryRoomAppointmentService.DeleteSurgeryRoomAppointment(oldSurgeryRoomAppointment))
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e}");
            }

            return BadRequest("Failed to delete the room");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SurgeryRoomAppointmentDto>> Put(int id, SurgeryRoomAppointmentDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

                var oldSurgeryRoomAppointment = await _surgeryRoomAppointmentService.GetSurgeryRoomAppointmentByIdAsync(id);
                if (oldSurgeryRoomAppointment == null) return NotFound($"Could not find camp with id of {id}");

                var updatedSurgeryRoomAppointment = _mapper.Map(model, oldSurgeryRoomAppointment);

                if (await _surgeryRoomAppointmentService.UpdateSurgeryRoomAppointment(updatedSurgeryRoomAppointment))
                {
                    return Ok(updatedSurgeryRoomAppointment);
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
