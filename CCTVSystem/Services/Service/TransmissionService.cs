using Services.DTO;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Linq;
using CctvDB;
using Domain;
using System.Threading.Tasks;
using System.IO;

namespace Services.Service
{
    public class TransmissionService : ITransmissionService
    {
        private readonly CctvDbContext _context;

        public TransmissionService(CctvDbContext context)
        {
            _context = context;
        }

        public async Task AddVideo(TransmissionDTO newVideo)
        { 
            await _context.Transmissions.AddAsync(Mapper.Map<TransmissionDTO, Transmission>(newVideo));
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfReady(TransmissionDTO Video)
        {
            var cam = _context.Transmissions.FirstOrDefault(x=>x.Id == Mapper.Map<TransmissionDTO, Transmission>(Video).Id);
            return cam.ReadyToDelete;
        }

        public void DeleteSavedVideos()
        {
            var range =_context.Transmissions.ToList();

            foreach(var item in range)
            {
                item.ReadyToDelete = true;
            }

            _context.SaveChanges();
        }

        public async Task<List<TransmissionDTO>> GetTrans()
        {
            var transList = await _context.Transmissions.Include(x => x.Camera).ToListAsync();
            var transListDto = Mapper.Map<List<Transmission>, List<TransmissionDTO>>(transList);
            return transListDto;
        }

        public async void DeleteCheckedTransmissionsAsync(int idTransmission)
        {
            var element = _context.Transmissions.Find(idTransmission);
            _context.Transmissions.Remove(element);
            _context.SaveChanges();

        }

    }
}
