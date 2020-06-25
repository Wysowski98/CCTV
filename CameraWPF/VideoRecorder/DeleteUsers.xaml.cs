using CCTVSystem.Client.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CCTVSystem
{
    /// <summary>
    /// Logika interakcji dla klasy DeleteUsers.xaml
    /// </summary>
    public partial class DeleteUsers : UserControl
    {
        static HttpClient client = new HttpClient();
        private List<GetUserProfileCommand> _users;
        public class Users
        {
            public bool IsSelected { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
        }

        public DeleteUsers()
        {
            InitializeComponent();
            getUsers();
        }

        private async void getUsers()
        {

            var response = await client.GetAsync("https://localhost:44309/api/Client/getUsers");
            //jezeli serwer wyslal pozytywna odpowiedz
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                _users = JsonConvert.DeserializeObject<List<GetUserProfileCommand>>(responseBody);

            }

            for (int i = 0; i < _users.Count; i++)
            {

                UsersList.Items.Add(_users[i]);
            }



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
