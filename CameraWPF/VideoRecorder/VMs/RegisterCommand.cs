using System;
using System.Collections.Generic;
using System.Text;

namespace CCTVSystem.Client.ViewModels
{
    public class RegisterCommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
