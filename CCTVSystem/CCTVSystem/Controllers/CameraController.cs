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
using NetCamera;

namespace CCTVSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CameraController : ControllerBase
    {
        private readonly ICameraService _service;
        private List<NetCamera.NetCamera> recCams;

        public CameraController(ICameraService service)
        {
            _service = service;
            recCams = new List<NetCamera.NetCamera>();
        }

        [HttpPost("AddCam")]
        public async Task<IActionResult> AddCamera([FromBody] CameraRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            var _camera = new Camera
            {
                IpAddress = req.Url,
                Client = _service.FindClient(req.clientId)
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

        [HttpPost("StartRec")]
        public IActionResult RecordCam([FromBody] CameraRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            //var _camera = _service.FindClientCamera(req);

            //if (_camera != null)
            //{
                NetCamera.NetCamera nc = new NetCamera.NetCamera(req.Url);
                //nc.cameraId = _camera.Id;
                recCams.Add(nc);
                nc.StartRecording();
                return Ok();
           // }
            //else
           // {
            //    return BadRequest("Client camera not found");
            //}
        }
    }
}