using CCTVSystem.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRecorder.VMs
{
    class HistoryCommand
    {
        public Camera Camera { get; set; }

        public DateTime RecordingDate { get; set; }

        public bool IsRecording { get; set; }

    }
}
