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
    public partial class LoginWindow : Window
    {
        static HttpClient client = new HttpClient();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var values = new LoginCommand
            {
                Username =  this.username.Text,
                Password= this.password.Password,
            };

            var myContent = JsonConvert.SerializeObject(values);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync("https://localhost:44309/api/Client/Login", byteContent);
            
            if(response.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show("Zalogowalo Cie");
            }
         
        }
    }
}
