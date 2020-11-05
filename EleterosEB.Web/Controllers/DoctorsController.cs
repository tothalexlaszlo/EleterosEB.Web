using Microsoft.AspNetCore.Mvc;
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
using Microsoft.AspNetCore.Routing;

namespace EleterosEB.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorService _doctorService;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public DoctorsController(DoctorService doctorService, IMapper mapper,
            LinkGenerator linkGenerator)
        {
            _doctorService = doctorService;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _doctorService.GetAllDoctorsAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<DoctorDto>> Post(DoctorDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existing = await _doctorService.GetDoctorByNameAsync(model.Name);
                    if (existing != null)
                    {
                        return BadRequest("The room is already exists!");
                    }

                    var location = _linkGenerator.GetPathByAction(HttpContext,
                        "Get", "Doctors",
                        new { name = model.Name });

                    if (string.IsNullOrWhiteSpace(location))
                    {
                        return BadRequest("Could not use current name");
                    }

                    var doctor = _mapper.Map<Doctor>(model);

                    if (await _doctorService.CreateDoctor(doctor))
                    {
                        return Created(location, doctor);
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
                var oldDoctor = await _doctorService.GetDoctorByIdAsync(id);
                if (oldDoctor == null) return NotFound();

                if (await _doctorService.DeleteDoctor(oldDoctor))
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
        public async Task<ActionResult<DoctorDto>> Put(int id, DoctorDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

                var oldDoctor = await _doctorService.GetDoctorByIdAsync(id);
                if (oldDoctor == null) return NotFound($"Could not find camp with id of {id}");

                var updatedDoctor = _mapper.Map(model, oldDoctor);

                if (await _doctorService.UpdateDoctor(updatedDoctor))
                {
                    return Ok(updatedDoctor);
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