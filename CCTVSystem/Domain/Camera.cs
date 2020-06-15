using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Camera
    {
        public int Id { get; set; }

        public string IpAddress { get; set; }

        public Client Client { get; set; }

    }
}
