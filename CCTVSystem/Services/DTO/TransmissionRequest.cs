using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTO
{
    public class TransmissionRequest
    {
        public DateTime RecordingDate { get; set; }

        public string FileName { get; set; }

        public CameraRequest CamRequest { get; set; }
    }
}
