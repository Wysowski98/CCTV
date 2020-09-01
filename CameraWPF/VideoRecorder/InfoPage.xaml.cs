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
using System.Windows.Shapes;

namespace CCTVSystem.Client
{
    /// <summary>
    /// Logika interakcji dla klasy Page1.xaml
    /// </summary>
    public partial class InfoPage : UserControl
    {
        static HttpClient client = new HttpClient();
        private ClientViewModel _loggedUser;
        private List<CameraViewModel> _clientCameras;
        private GetUserProfileCommand _profile;

        public InfoPage(ClientViewModel loggedUser)
        {
            InitializeComponent();
            _loggedUser = loggedUser;
            getUserProfile();
        }
       

        private async void getUserProfile()
        {
            var values = new UserId
            {
                id = this._loggedUser.Id,
            };
            //proces wysylania żądania do serwera
            var myContent = JsonConvert.SerializeObject(values);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync("https://localhost:44309/api/Client/GetUserProfile", byteContent);
            //jezeli serwer wyslal pozytywna odpowiedz
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                _profile = JsonConvert.DeserializeObject<GetUserProfileCommand>(responseBody);
            }
            //przypisanie wartości do boxów
            username1.Text = _profile.Username;
            email1.Text = _profile.Email;
       
            role1.Items.Add(_profile.Roles);
            id1.Items.Add(_profile.TransmissionId);
        }

        private void role1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}