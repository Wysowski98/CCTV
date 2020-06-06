using CCTVSystem.Client.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Channels;
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

        private async void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (this.Password.Equals(this.ConfirmPassword))
            {
                MessageBox.Show("Hasła nie są jednakowe!");
            }
            else
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
                    LoginWindow lw = new LoginWindow();
                    lw.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("Bląd rejestracji!");
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }

        private void TextBoxFirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "First name";
                tb.GotFocus += TextBox_GotFocus;
            }
        }

        private void TextBoxLastName_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "Last name";
                tb.GotFocus += TextBox_GotFocus;
            }
        }

        private void TextBoxUsername_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "Username";
                tb.GotFocus += TextBox_GotFocus;
            }
        }

        private void TextBoxEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "Email address";
                tb.GotFocus += TextBox_GotFocus;
            }
        }

        private void TextBoxPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "Password";
                tb.GotFocus += TextBox_GotFocus;
            }
        }

        private void TextBoxConfirmPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "Confirm password";
                tb.GotFocus += TextBox_GotFocus;
            }
        }
    }
}
