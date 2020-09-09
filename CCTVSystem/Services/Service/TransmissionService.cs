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

        public async Task AddVideo(Transmission newVideo)
        { 
            await _context.Transmissions.AddAsync(newVideo);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfReady(Transmission Video)
        {
            var cam = _context.Transmissions.FirstOrDefault(x => x.Id == Video.Id);
            return cam.ReadyToDelete;
        }

        public void DeleteSavedVideos()
        {
            var range = _context.Transmissions.ToList();

            //foreach (var item in range)
            //{
            //    item.ReadyToDelete = true;
            //}

            _context.SaveChanges();
        }

        public async Task<List<Transmission>> GetTrans()
        {
            var transList = await _context.Transmissions.ToListAsync();

            return transList;
        }

        public async void DeleteCheckedTransmissionsAsync(int idTransmission)
        {
            var element = _context.Transmissions.Find(idTransmission);
            _context.Transmissions.Remove(element);
            _context.SaveChanges();

        }


        public async void DeleteCheckedTransmissionsByAdminAsync(int idTransmission)
        {
            var element = _context.Transmissions.Find(idTransmission);
            element.ReadyToDelete = true;
            _context.Transmissions.Update(element);
            _context.SaveChanges();

        }

    }
}
