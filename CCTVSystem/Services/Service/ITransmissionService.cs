using Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Services.Service
{
    public interface ITransmissionService
    {
        Task AddVideo(Transmission newVideo);

        Task<bool> CheckIfReady(Transmission newVideo);

        void DeleteSavedVideos();

        Task<List<Transmission>> GetTrans();

        void DeleteCheckedTransmissionsAsync(int idTransmission);

        void DeleteCheckedTransmissionsByAdminAsync(int idTransmission);
    }
}
