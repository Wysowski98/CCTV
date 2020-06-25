using Domain;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public interface IClientService
    {
        Task<List<ClientDTO>> GetClients();

        int GetClientTransmission(string id);

    }
}
