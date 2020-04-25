using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Cctv
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public string Description { get; set; }

        public Client Client { get; set; }
        public int ClientId { get; set; }
    }
}
