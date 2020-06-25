using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCTVSystem.Client.ViewModels
{
    class GetUserProfileCommand
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
        public int TransmissionId { get; set; }
    }

    class UserId
    {
        public string id { get; set; }
    }
    
}
