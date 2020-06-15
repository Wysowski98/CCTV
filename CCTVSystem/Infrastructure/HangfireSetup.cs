using Hangfire;
using Services.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Infrastructure
{
    public class HangfireSetup
    {
        private readonly ITransmissionService _service;
        public HangfireSetup(ITransmissionService service)
        {
            _service = service;
        }
        public void ScheduleReccurringCleaning()
        {
            _service.DeleteSavedVideos();
        }
    }
}
