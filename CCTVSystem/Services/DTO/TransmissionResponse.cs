using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTO
{
    public class TransmissionResponse
    {
            public int Id { get; set; }

            public DateTime RecordingDate { get; set; }

            public string FileName { get; set; }

            public bool IsRecording { get; set; }
            public bool ReadyToDelete { get; set; }

            public int Hours { get; set; }

            public int Minutes { get; set; }

            public int CameraId { get; set; }
    }
}
