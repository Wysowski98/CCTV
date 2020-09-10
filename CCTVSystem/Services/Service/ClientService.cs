using Services.DTO;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Linq;
using CctvDB;
using Domain;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Collections;

namespace Services.Service
{
    public class ClientService : IClientService
    {
        private readonly CctvDbContext _context;
        private readonly UserManager<Client> _userManager;
        private readonly SignInManager<Client> _signInManager;
        private readonly ICameraService _cam_service;

        public ClientService(CctvDbContext context, UserManager<Client> userManager, SignInManager<Client> signInManager)
        {
            _context = context;
        }

        public int GetClientTransmission(string id)
        {
            var client =  _context.Clients.Include(c => c.Transmission).First(c => c.Id == id);

            if (client.Transmission != null)
            {
                return client.Transmission.Id;
            }
            else
                return 0;
        }

        public void DeleteUserTransmissions(int CamId)
        {
            var transList = _context.Transmissions.Where(trans => trans.CameraId == CamId).ToList();
            foreach (var transmission in transList)
            {             
                    transmission.ReadyToDelete = true;
                    transmission.CameraId = null;
                    _context.Transmissions.Update(transmission);
                    _context.SaveChanges();
                
            }
               
        }

        public async Task<List<ClientDTO>> GetClients()
        {
            var clientList = await _context.Clients.ToListAsync();
            var clientListDto = Mapper.Map<List<Client>, List<ClientDTO>>(clientList);
            return clientListDto;
        }

        public async void DeleteCheckedUserAsync(string username)
        {
            var user = _context.Clients.First(u => u.UserName == username);

            var camList = _context.Cameras.Where(cam => cam.Client.Id == user.Id).ToList();
 
            foreach (var camera in camList)
            {
                int id = camera.Id;
                DeleteUserTransmissions(id);
                Camera camToDelete = _context.Cameras.Find(id);
                _context.Cameras.Remove(camToDelete);
                _context.SaveChanges();
            }

                _context.Clients.Remove(user);
                _context.SaveChanges();
         
        }
    }
}
