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

        public TransController(ITransmissionService service)
        {
            _tr = service;
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
    }
}