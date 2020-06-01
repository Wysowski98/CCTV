using System;
using System.Collections.Generic;
using System.Text;

namespace CCTVSystem.Client.ViewModels
{
    public class ClientViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<CctvViewModel> FavouriteCctvs { get; set; }
    }
}
