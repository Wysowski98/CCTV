using Domain;
using Services.Service;
using Services.DTO;
using AutoMapper;

namespace Infrastructure
{
    public class MapperSetup
    {
        public static void Configurate()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Client, ClientDTO>();
                cfg.CreateMap<ClientDTO, Client>();
            });
        }
    }
}
