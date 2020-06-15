using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class Client: IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Transmission Transmission { get; set; }
    }
}
