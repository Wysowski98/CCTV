using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class Client: IdentityUser
    {
        public List<Cctv> FavouriteCctvs { get; set; }

        public string LastViewedStream { get; set; }
    }
}
