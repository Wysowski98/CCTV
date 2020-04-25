using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTO
{
    public class CctvDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public string Description { get; set; }

        public ClientDTO Client { get; set; }
        public int ClientId { get; set; }
    }
}
