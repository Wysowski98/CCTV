using Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public interface ITransmissionService
    {
        Task AddVideo(TransmissionDTO newVideo);
        void DeleteSavedVideos();
    }
}
