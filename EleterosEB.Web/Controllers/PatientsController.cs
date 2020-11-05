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
    public class PatientsController: ControllerBase
    {
        private readonly PatientService _patientServices;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public PatientsController(PatientService patientServices, IMapper mapper,
            LinkGenerator linkGenerator)
        {
            _patientServices = patientServices;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _patientServices.GetAllPatientsAsync();
                return Ok(_mapper.Map<List<PatientDto>>(result));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PatientDto>> Post(PatientDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existing = await _patientServices.GetPatientByNameAsync(model.Name);
                    if (existing != null)
                    {
                        return BadRequest("The patient is already exists!");
                    }

                    var location = _linkGenerator.GetPathByAction(HttpContext,
                        "Get", "Patients",
                        new { name = model.Name });

                    if (string.IsNullOrWhiteSpace(location))
                    {
                        return BadRequest("Could not use current name");
                    }

                    var patient = _mapper.Map<Patient>(model);

                    if (await _patientServices.CreatePatient(patient))
                    {
                        return Created(location, patient);
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
                var oldPatient = await _patientServices.GetPatientsByIdAsync(id);
                if (oldPatient == null) return NotFound();

                if (await _patientServices.DeletePatient(oldPatient))
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
        public async Task<ActionResult<PatientDto>> Put(int id, PatientDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

                var oldPatient = await _patientServices.GetPatientsByIdAsync(id);
                if (oldPatient == null) return NotFound($"Could not find patient with id of {id}");

                var updatedPatient = _mapper.Map(model, oldPatient);

                if (await _patientServices.UpdatePatient(updatedPatient))
                {
                    return Ok(updatedPatient);
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
