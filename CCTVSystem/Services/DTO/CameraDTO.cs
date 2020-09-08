using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTO
{
    public class CameraDTO
    {
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public int ClientId { get; set; }
    }
}
