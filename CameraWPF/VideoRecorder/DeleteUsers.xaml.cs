using CCTVSystem.Client.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
            public string Id { get; set; }
            public bool IsSelected { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Roles { get; set; }
        }

        public DeleteUsers()
        {
            InitializeComponent();
            getUsers();
        }

        private async void getUsers()
        {

            var response = await client.GetAsync("https://localhost:44309/api/Client");
            //jezeli serwer wyslal pozytywna odpowiedz
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                _users = JsonConvert.DeserializeObject<List<GetUserProfileCommand>>(responseBody);

            }

            if (_users != null)
            {
                foreach (GetUserProfileCommand gu in _users)
                {
                    Users u = new Users();               
                    u.Id = gu.Id;
                    u.Username = gu.Username;
                    u.Email = gu.Email;

                    Console.WriteLine(u.Id);
                    Console.WriteLine(gu.Id);

                    UsersList.Items.Add(u);                    

                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            List<Users> objToDelete = new List<Users>();
            foreach (Users u in UsersList.Items)
            {
                if (u.IsSelected == true)
                {
                    deleteUser(u.Username);
                    objToDelete.Add(u);
                }
            }

            if (!objToDelete.Any())
            {
                MessageBox.Show("There is nothing to delete");
            }
            else
            {
                foreach (Users u in objToDelete)
                {
                    UsersList.Items.Remove(u);
                }
            }

        }

        private async void deleteUser(string username)
        {
          
                var response = await client.DeleteAsync("https://localhost:44309/api/Client/" + username);
             
        }

    }
}
