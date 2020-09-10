using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Transmission
    {
        public int Id { get; set; }

        public DateTime RecordingDate { get; set; }
        
        public string FileName { get; set; }

        public bool IsRecording { get; set; }

        public bool ReadyToDelete { get; set; }

        public int Hours { get; set; }

        public int Minutes { get; set; }

        public System.Nullable<int> CameraId { get; set; }
    }
}
