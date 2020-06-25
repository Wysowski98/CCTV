using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class UserProfile
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public IList<string> Role { get; set; }

        public int TransmissionId { get; set; }
    }
}
