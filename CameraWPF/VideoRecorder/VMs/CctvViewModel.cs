using System;
using System.Collections.Generic;
using System.Text;

namespace CCTVSystem.Client.ViewModels
{
    public class CctvViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public string Description { get; set; }

        public CctvViewModel Client { get; set; }
        public int ClientId { get; set; }
    }
}
