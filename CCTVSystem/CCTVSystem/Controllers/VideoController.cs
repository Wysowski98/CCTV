using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Service;

namespace CCTVSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _service;

        public VideoController(IVideoService service)
        {
            _service = service;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddClients([FromBody] VideosDTO video)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            await _service.AddVideo(video);

            return Ok();
        }
    }
}