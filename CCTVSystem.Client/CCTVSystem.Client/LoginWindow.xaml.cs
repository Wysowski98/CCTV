using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
            var values = new Dictionary<string, string>
            {
                { "username", this.username.Text },
                { "password", this.password.Password }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("https://localhost:44309/api/Login", content);

            if(response.StatusCode == HttpStatusCode.BadRequest)
            {
                MessageBox.Show(response.Content.ToString());
            }
        }
    }
}
