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
    public class ClientService : IClientService
    {
        private readonly CctvDbContext _context;
        private readonly UserManager<Client> _userManager;
        private readonly SignInManager<Client> _signInManager;

        public ClientService(CctvDbContext context, UserManager<Client> userManager, SignInManager<Client> signInManager)
        {
            _context = context;
        }

        public async Task<List<ClientDTO>> GetClients()
        {
            var clientList = await _context.Clients.ToListAsync();
            var clientListDto = Mapper.Map<List<Client>, List<ClientDTO>>(clientList);
            return clientListDto;
        }
    }
}
