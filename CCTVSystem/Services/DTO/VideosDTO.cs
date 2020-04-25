using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTO
{
    public class VideosDTO
    {
        public int Id { get; set; }
        public string VideoLink { get; set; }
        public double Size { get; set; }
        public float Duration { get; set; }
    }
}
