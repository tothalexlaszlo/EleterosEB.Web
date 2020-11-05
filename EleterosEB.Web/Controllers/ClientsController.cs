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
    public class ClientsController: ControllerBase
    {
        private readonly ClientService _clientServices;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public ClientsController(ClientService clientServices, IMapper mapper, 
            LinkGenerator linkGenerator)
        {
            _clientServices = clientServices;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet] 
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _clientServices.GetAllClientsAsync();
                //return Ok(result);
                return Ok(_mapper.Map<List<ClientDto>>(result));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ClientDto>> Post(ClientDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existing = await _clientServices.GetClientByNameAsync(model.FullName);
                    if (existing != null)
                    {
                        return BadRequest("The client is already exists!");
                    }

                    var location = _linkGenerator.GetPathByAction(HttpContext,
                        "Get", "Clients",
                        new { name = model.FullName });

                    if (string.IsNullOrWhiteSpace(location))
                    {
                        return BadRequest("Could not use current name");
                    }

                    var client = _mapper.Map<Client>(model);

                    if (await _clientServices.CreateClient(client))
                    {
                        return Created(location, client);
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
                var oldClient = await _clientServices.GetClientsByIdAsync(id);
                if (oldClient == null) return NotFound();

                if (await _clientServices.DeleteClient(oldClient))
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e}");
            }

            return BadRequest("Failed to delete the client");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClientDto>> Put(int id, ClientDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

                var oldClient = await _clientServices.GetClientsByIdAsync(id);
                if (oldClient == null) return NotFound($"Could not find client with id of {id}");

                var updatedCkClient = _mapper.Map(model, oldClient);

                if (await _clientServices.UpdateClient(updatedCkClient))
                {
                    return Ok(updatedCkClient);
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
