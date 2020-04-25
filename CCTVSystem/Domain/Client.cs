using System;
using System.Collections.Generic;

namespace Domain
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreationDate { get; set; }
        public string MailAddress { get; set; }
        public List<Cctv> FavouriteCctvs { get; set; }
    }
}
