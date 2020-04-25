using CCTVSystem.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CCTVSystem.Client
{
    /// <summary>
    /// Interaction logic for AddClient.xaml
    /// </summary>
    public partial class AddClient : Page
    {
        static HttpClient client = new HttpClient();
        public AddClient()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var newClient = new ClientViewModel
            {
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                MailAddress = Email.Text,
                CreationDate = DateTime.Now,
                FavouriteCctvs = new List<CctvViewModel>(),
            };

            await GetAPIAsync(newClient);
        }

        static async Task GetAPIAsync(ClientViewModel newClient)
        {
            var myContent = JsonConvert.SerializeObject(newClient);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync("https://localhost:44309/api/Client/Add", byteContent);
            string resultContent = await response.Content.ReadAsStringAsync();
        }
    }
}
