using Services.DTO;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Linq;
using CctvDB;
using Domain;
using System.Threading.Tasks;

namespace Services.Service
{
    public class CameraService : ICameraService
    {
        private readonly CctvDbContext _context;

        public CameraService(CctvDbContext context)
        {
            _context = context;
        }

        public async Task AddCamera(Camera newCamera)
        {
            await _context.Cameras.AddAsync(newCamera);
            await _context.SaveChangesAsync();
        }

        public Client FindClient(string id)
        {
            return _context.Clients.FirstOrDefault(cli => cli.Id == id);
        }

        public async Task<List<CameraDTO>> GetClientCameras(Client client)
        {
            var cameraList = await _context.Cameras.Where(cam => cam.Client.Id == client.Id).ToListAsync();
            var cameraListDto = Mapper.Map<List<Camera>, List<CameraDTO>>(cameraList);
            return cameraListDto;
        }
    }
}
