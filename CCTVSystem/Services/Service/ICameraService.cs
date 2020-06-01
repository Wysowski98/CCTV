using Domain;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public interface ICameraService
    {
        Task AddCamera(Camera newCamera);
        Task<List<CameraDTO>> GetClientCameras(Client client);
    }
}
