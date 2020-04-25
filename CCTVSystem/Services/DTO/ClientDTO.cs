using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreationDate { get; set; }
        public string MailAddress { get; set; }
        public List<CctvDTO> FavouriteCctvs { get; set; }
    }
}
