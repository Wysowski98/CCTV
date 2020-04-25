using Services.DTO;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Linq;
using CctvDB;
using Domain;
using System.Threading.Tasks;

namespace Services.Service
{
    public class ClientService : IClientService
    {
        private readonly CctvDbContext _context;

        public ClientService(CctvDbContext context)
        {
            _context = context;
        }

        public async Task AddClient(ClientDTO newClient)
        {
            await _context.Clients.AddAsync(Mapper.Map<ClientDTO, Client>(newClient));
            await _context.SaveChangesAsync();
        }

        public async Task<List<ClientDTO>> GetClients()
        {
            var clientList = await _context.Clients.Include(x =>x.FavouriteCctvs).ToListAsync();
            var clientListDto = Mapper.Map<List<Client>, List<ClientDTO>>(clientList);
            return clientListDto;
        }
    }
}
