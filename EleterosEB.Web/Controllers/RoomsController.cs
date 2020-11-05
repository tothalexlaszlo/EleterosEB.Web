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
    public class RoomsController: ControllerBase
    {
        private readonly RoomService _roomServices;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public RoomsController(RoomService roomService, IMapper mapper, 
            LinkGenerator linkGenerator)
        {
            _roomServices = roomService;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result =   await _roomServices.GetAllRoomsAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e}");
            }
        }

        //[HttpGet("{name}")]
        //public async Task<ActionResult<Room>> Get(string name)
        //{
        //    try
        //    {
        //        var result = await _roomServices.GetRoomByNameAsync(name);
        //        return Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e}");
        //    }
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> Get(int id)
        {
            try
            {
                var result = await _roomServices.GetRoomByIdAsync(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Room>> Post(RoomDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existing = await _roomServices.GetRoomByNameAsync(model.Name);
                    if (existing != null)
                    {
                        return BadRequest("The room is already exists!");
                    }

                    var location = _linkGenerator.GetPathByAction(HttpContext,
                        "Get", "Rooms",
                        new { name = model.Name });

                    if (string.IsNullOrWhiteSpace(location))
                    {
                        return BadRequest("Could not use current name");
                    }

                    var room = _mapper.Map<Room>(model);

                    if (await _roomServices.CreateRoom(room))
                    {
                        return Created(location, room);
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
                var oldRoom = await _roomServices.GetRoomByIdAsync(id);
                if (oldRoom == null) return NotFound();

                if (await _roomServices.DeleteRoom(oldRoom))
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
        public async Task<ActionResult<RoomDto>> Put(int id, RoomDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

                var oldRoom = await _roomServices.GetRoomByIdAsync(id);
                if (oldRoom == null) return NotFound($"Could not find camp with id of {id}");

                var updatedRoom = _mapper.Map(model, oldRoom);

                if (await _roomServices.UpdateRoom(updatedRoom))
                {
                    return Ok(updatedRoom);
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