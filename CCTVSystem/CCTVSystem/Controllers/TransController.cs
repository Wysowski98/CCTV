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
    public class TransController : ControllerBase
    {
        private readonly ITransmissionService _tr;
        private readonly ICameraService _cs;

        public TransController(ITransmissionService service, ICameraService cameraService)
        {
            _tr = service;
            _cs = cameraService;
        }


        [HttpGet("getTranss")]
        public async Task<IActionResult> getTranss()
        {
            var classes = await _tr.GetTrans();
            if (classes.Any())
            {
                /*
                List<TransmissionResponse> trList = new List<TransmissionResponse>();
                foreach(Transmission tDTO in classes)
                {
                    TransmissionResponse tr = new TransmissionResponse();
                    tr.Id = tDTO.Id;
                    tr.IsRecording = tDTO.IsRecording;
                    tr.Hours = tDTO.Hours;
                    tr.Minutes = tDTO.Minutes;
                    tr.ReadyToDelete = tDTO.ReadyToDelete;
                    tr.RecordingDate = tDTO.RecordingDate;
                    tr.FileName = tDTO.FileName;
                    tr.CameraId = tDTO.CameraId;
                    trList.Add(tr);
                }
                */
                return Ok(classes);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("addTranss")]
        public async Task<IActionResult> addTranss([FromBody] TransmissionRequest trq)
        {
            Transmission t = new Transmission();
            var cam = _cs.FindClientCamera(trq.CamRequest);
            if (cam == null)
                return BadRequest("Błędne dane dotyczące kamery!");
            else
            {
                t.CameraId = cam.Id;       
                t.FileName = trq.FileName;
                t.IsRecording = true;
                t.RecordingDate = trq.RecordingDate;
                await _tr.AddVideo(t);
                return Ok();
            }
        }

        [HttpDelete("{idTransmission}")]
        public async Task<IActionResult> DeleteTransmission(int idTransmission)
        {
            _tr.DeleteCheckedTransmissionsAsync(idTransmission);
            return Ok();

        }

        [HttpDelete("/admin/{idTransmission}")]
        public async Task<string> DeleteCheckedTransmissionsByAdmin(int idTransmission)
        {
            var trans = await _tr.DeleteCheckedTransmissionsByAdminAsync(idTransmission);
            return trans.FileName;

        }
    }
}