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
    public class AppointmentTypesController: ControllerBase
    {
        private readonly AppointmentTypeService _appointmentTypeService;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public AppointmentTypesController(AppointmentTypeService appointmentTypeService, IMapper mapper,
            LinkGenerator linkGenerator)
        {
            _appointmentTypeService = appointmentTypeService;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _appointmentTypeService.GetAllAppointmentTypesAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e}");
            }
        }


        [HttpPost]
        public async Task<ActionResult<AppointmentTypeDto>> Post(AppointmentTypeDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existing = await _appointmentTypeService.GetAppointmentTypeByNameAsync(model.Name);
                    if (existing != null)
                    {
                        return BadRequest("The appointment type is already exists!");
                    }

                    var location = _linkGenerator.GetPathByAction(HttpContext,
                        "Get", "AppointmentTypes",
                        new { name = model.Name });

                    if (string.IsNullOrWhiteSpace(location))
                    {
                        return BadRequest("Could not use current name");
                    }

                    var appointmentType = _mapper.Map<AppointmentType>(model);

                    if (await _appointmentTypeService.CreateAppointmentType(appointmentType))
                    {
                        return Created(location, appointmentType);
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
                var oldAppointmentType = await _appointmentTypeService.GetAppointmentTypeByIdAsync(id);
                if (oldAppointmentType == null) return NotFound();

                if (await _appointmentTypeService.DeleteAppointmentType(oldAppointmentType))
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e}");
            }

            return BadRequest("Failed to delete the appointment type");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AppointmentTypeDto>> Put(int id, AppointmentTypeDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

                var oldAppointmentType = await _appointmentTypeService.GetAppointmentTypeByIdAsync(id);
                if (oldAppointmentType == null) return NotFound($"Could not find appointment type with id of {id}");

                var updatedAppointmentType = _mapper.Map(model, oldAppointmentType);

                if (await _appointmentTypeService.UpdateAppointmentType(updatedAppointmentType))
                {
                    return Ok(updatedAppointmentType);
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
