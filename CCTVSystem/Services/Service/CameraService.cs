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

        public async Task<List<CameraDTO>> GetClientCameras(Client client)
        {
            var cameraList = await _context.Cameras.Where(cam => cam.Client == client).ToListAsync();
            var cameraListDto = Mapper.Map<List<Camera>, List<CameraDTO>>(cameraList);
            return cameraListDto;
        }
    }
}
