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
    public class VideoService : IVideoService
    {
        private readonly CctvDbContext _context;

        public VideoService(CctvDbContext context)
        {
            _context = context;
        }

        public async Task AddVideo(VideosDTO newVideo)
        { 
            await _context.Video.AddAsync(Mapper.Map<VideosDTO, Videos>(newVideo));
            await _context.SaveChangesAsync();
        }

        public void DeleteSavedVideos()
        {
            var range =_context.Video.ToList();
            _context.Video.RemoveRange(range);
            _context.SaveChanges();
            DeleteVideosOnHardDrive();
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
    }
}
