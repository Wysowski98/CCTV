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
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

                if (signInResult.Succeeded)
                {
                    return Ok(user);
                }
                else
                {
                    return BadRequest("Incorrect password");
                }
            }

            return BadRequest("Incorrect username");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var user = new Client
            {
                UserName = request.Username,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "USER");
                return Ok();
            }

            return BadRequest("Failed to register.");
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var validators = _userManager.PasswordValidators;
            foreach (var validator in validators)
            {
                //validator sprawdza czy nowe haslo spelnia wymagania
                var check = await validator.ValidateAsync(_userManager, null, request.newPassword2);
                if (!check.Succeeded)
                {
                    return BadRequest("Nowe haslo nie spelnia wymagan");
                }
                //szuka uzytkownika po przekazanym id
                var user = await _userManager.FindByIdAsync(request.id);
                //zmiana hasla
                var result = await _userManager.ChangePasswordAsync(user, request.oldPassword, request.newPassword2);
                if (result.Succeeded)
                {
                    return Ok("Pomyślnie zmieniono hasło");
                }
            }
            return BadRequest("Nie udało się zmienić hasła");
        }

        [HttpPost("GetUserProfile")]
        public async Task<IActionResult> GetUserProfile(GetUserProfileRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.id);
            var username = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var role = await _userManager.GetRolesAsync(user);
            var trId = _service.GetClientTransmission(request.id);

            var userProfile = new UserProfile
            {
                Username = username,
                Email = email,
                Role = role.FirstOrDefault(),
                TransmissionId = trId
            };
            return Ok(userProfile);
        }


        [HttpDelete("{idUser}")]
        public async Task<IActionResult> DeleteUser(string idUser)
        {
            _service.DeleteCheckedUserAsync(idUser);
            return Ok();

        }
    }
}