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
    public partial class RegisterWindow : Window
    {
        static HttpClient client = new HttpClient();
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var values = new RegisterCommand
            {
                FirstName = this.FirstName.Text,
                LastName = this.LastName.Text,
                Email = this.Email.Text,
                Username = this.Username.Text,
                Password = this.Password.Text,
            };

            var myContent = JsonConvert.SerializeObject(values);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync("https://localhost:44309/api/Client/Register", byteContent);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show("Zarejestrowalo Cie");
            }
            else
                MessageBox.Show("Bląd rejestracji!");

            LoginWindow windowTwo = new LoginWindow();

            windowTwo.Show();
            this.Close();
        }
    }
}
