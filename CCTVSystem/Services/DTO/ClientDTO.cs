using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTO
{
    public class ClientDTO: IdentityUser
    {
        public List<CctvDTO> FavouriteCctvs { get; set; }

        public string LastViewedStream { get; set; }
    }
}
