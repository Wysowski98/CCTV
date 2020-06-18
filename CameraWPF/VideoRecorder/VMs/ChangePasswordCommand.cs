using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCTVSystem.Client.ViewModels
{
    class ChangePasswordCommand
    {
        public string id { get; set; }
        public string oldPassword { get; set; }
        public string newPassword1 { get; set; }
        public string newPassword2 { get; set; }

    }
    
}
