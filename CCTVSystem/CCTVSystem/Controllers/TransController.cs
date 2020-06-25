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
                return Ok(classes);
            }
            else
            {
                return NotFound();
            }
        }
    }
}