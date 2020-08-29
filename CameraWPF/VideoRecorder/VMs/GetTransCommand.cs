using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCTVSystem.Client.ViewModels
{
    class GetTransCommand
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public DateTime RecordingDate { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int CameraId { get; set; }

    }
}
