using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Service;
using Services.DTO;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace CCTVSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _service;
        private readonly UserManager<Client> _userManager;
        private readonly SignInManager<Client> _signInManager;

        public ClientController(IClientService service, UserManager<Client> userManager, SignInManager<Client> signInManager)
        {
            _service = service;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var classes = await _service.GetClients();
            if (classes.Any())
            {
                return Ok(classes);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (signInResult.Succeeded)
                {
                    return Ok();
                }
            }

            return BadRequest("Failed to log in.");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            var user = new Client
            {
                UserName = username
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                // sign in
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (signInResult.Succeeded)
                {
                    return Ok();
                }
            }

            return BadRequest("Failed to register.");
        }
    }
}