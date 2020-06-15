using Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class CameraRequest
    {
        public string Url { get; set; }
       
        public ClientDTO Client { get; set; }
    }
}
