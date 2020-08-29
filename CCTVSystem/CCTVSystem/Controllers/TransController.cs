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
                List<TransmissionResponse> trList = new List<TransmissionResponse>();
                foreach(TransmissionDTO tDTO in classes)
                {
                    TransmissionResponse tr = new TransmissionResponse();
                    tr.Id = tDTO.Id;
                    tr.IsRecording = tDTO.IsRecording;
                    tr.Hours = tDTO.Hours;
                    tr.Minutes = tDTO.Minutes;
                    tr.ReadyToDelete = tDTO.ReadyToDelete;
                    tr.RecordingDate = tDTO.RecordingDate;
                    tr.FileName = tDTO.FileName;
                    tr.CameraId = tDTO.Camera.Id;
                    trList.Add(tr);
                }
                return Ok(trList);
            }
            else
            {
                return NotFound();
            }
        }

        //Not working yet ( ! Cannot insert explicit value for identity column in table 'Cameras' when IDENTITY_INSERT is set to OFF. ! )
        [HttpPost("addTranss")]
        public async Task<IActionResult> addTranss([FromBody] TransmissionRequest trq)
        {
            TransmissionDTO tDTO = new TransmissionDTO();
            var cam = _cs.FindClientCamera(trq.CamRequest);
            
            tDTO.Camera = Mapper.Map<Camera, CameraDTO>(cam);
            if (tDTO.Camera == null)
                return BadRequest("Błędne dane dotyczące kamery!");
            else
            {
                tDTO.FileName = trq.FileName;
                tDTO.IsRecording = true;
                tDTO.RecordingDate = trq.RecordingDate;
                await _tr.AddVideo(tDTO);
                return Ok();
            }
        }

        [HttpDelete("{idTransmission}")]
        public async Task<IActionResult> DeleteTransmission(int idTransmission)
        {
            _tr.DeleteCheckedTransmissionsAsync(idTransmission);
            return Ok();

        }
    }
}