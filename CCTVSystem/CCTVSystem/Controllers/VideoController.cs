using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Service;
using Domain;

namespace CCTVSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly ITransmissionService _service;

        public VideoController(ITransmissionService service)
        {
            _service = service;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddClients([FromBody] Transmission video)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            await _service.AddVideo(video);

            return Ok();
        }


        [HttpPost("RecReady")]
        public async Task<IActionResult> CheckIfReadyCam([FromBody] Transmission newVideo)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            return Ok(await _service.CheckIfReady(newVideo));
        }
    }
}