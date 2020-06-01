using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.DTO;
using Services.Service;

namespace CCTVSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CameraController : ControllerBase
    {
        private readonly ICameraService _service;

        public CameraController(ICameraService service)
        {
            _service = service;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddCamera([FromBody] CameraRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            var _camera = new Camera
            {
                IpAddress = req.Url,
                Client = Mapper.Map<ClientDTO, Client>(req.Client)
            };

            await _service.AddCamera(_camera);

            return Ok();
        }

        [HttpPost("GetCams")]
        public async Task<IActionResult> GetCameras(ClientDTO clientDTO)
        {        
            var classes = await _service.GetClientCameras(Mapper.Map<ClientDTO, Client>(clientDTO));
            if (classes.Any())
            {
                return Ok(classes);
            }
            else
            {
                return NotFound();
            }
        }
    }
}