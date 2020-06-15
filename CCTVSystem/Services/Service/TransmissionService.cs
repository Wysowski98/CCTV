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

        // public void DeleteSavedVideos()
        //  {
        //      var range =_context.Transmissions.ToList();
        //      _context.Transmissions.RemoveRange(range);
        //      _context.SaveChanges();
        //     DeleteVideosOnHardDrive();
        //  }
       

        public void DeleteSavedVideos()
        {
            var range = _context.Transmissions.ToList();

          //  foreach (var item in range)
          //  {
         //       item.ReadyToDelete = true;
          //  }

            _context.SaveChanges();
        }


        private void DeleteVideosOnHardDrive()
        {
            try
            {
                var rootFolder = @"C:\Users\Maciej\source\repos\SavedVideos";
                var videoFile = "Video.txt";
                if (File.Exists(Path.Combine(rootFolder, videoFile)))
                {

                    File.Delete(Path.Combine(rootFolder, videoFile));
                    Console.WriteLine("File deleted.");
                }
                else Console.WriteLine("File not found");
            }
            catch (IOException ioExp)
            {
                Console.WriteLine(ioExp.Message);
            }
        }

        public async Task<List<TransmissionDTO>> GetTrans() {
            var transList = await _context.Transmissions.Include(x=>x.Camera).ToListAsync();
            var transListDto = Mapper.Map<List<Transmission>, List<TransmissionDTO>>(transList);
            return transListDto;
        }
    }
}
