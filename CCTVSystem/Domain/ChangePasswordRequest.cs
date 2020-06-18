using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ChangePasswordRequest
    {
        public string id { get; set; }
        public string oldPassword { get; set; }
        public string newPassword1 { get; set; }
        public string newPassword2 { get; set; }
    }
}
