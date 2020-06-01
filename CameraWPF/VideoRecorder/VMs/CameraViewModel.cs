using System;
using System.Collections.Generic;
using System.Text;

namespace CCTVSystem.Client.ViewModels
{
    public class CameraViewModel
    {
        public int Id { get; set; }

        public string IpAddress { get; set; }

        public ClientViewModel Client { get; set; }
    }
}
