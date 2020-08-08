using System;
using System.Collections.Generic;
using System.Text;

namespace CCTVSystem.Client.ViewModels
{
    class TransmissionCommand
    {
        public DateTime RecordingDate { get; set; }
        public CameraCommand CamRequest { get; set; }
        public string FileName { get; set; }
    }
}
