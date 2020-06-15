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
    /// Logika interakcji dla klasy password.xaml
    /// </summary>
    public partial class password : UserControl
    {

        static HttpClient client = new HttpClient();
        public System.Security.SecureString SecurePassword { get; }
        private ClientViewModel _loggedUser;

        public password()
        {
            InitializeComponent();
        }

        public password(ClientViewModel loggedUser)
        {
            InitializeComponent();
            _loggedUser = loggedUser;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //Jezeli wszystkie pola sa wypelnione
            
            if (!String.IsNullOrWhiteSpace(new2.Password) && !String.IsNullOrWhiteSpace(new3.Password) && !String.IsNullOrWhiteSpace(old.Password))
            {
                //Jezeli są takie same
                if (new2.Password == new3.Password)
                {
                    //przypisanie zmiennych do wyslania serwerowi
                    var values = new ChangePasswordCommand
                    {
                        id = this._loggedUser.Id,
                        oldPassword = this.old.Password,
                        newPassword1 = this.new2.Password,
                        newPassword2 = this.new3.Password
                    };
                    //proces wysylania żądania do serwera
                    var myContent = JsonConvert.SerializeObject(values);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync("https://localhost:44309/api/Client/ChangePassword", byteContent);
                    //jezeli serwer wyslal pozytywna odpowiedz
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("Pomyślna zmiana hasła");
                        MainWindow mw = new MainWindow(_loggedUser);
                        mw.Show();
                    }
                    else
                    {
                        MessageBox.Show("Nowe haslo nie spelnia wymagan");
                    }
                }
                else
                {
                    MessageBox.Show("Podane hasła różnią się, wprowadź identyczne");
                }
            }//Jezeli nie zostaly wypelnione
            else
            {
                MessageBox.Show("Wypełnij wszystkie pola");
            }
        }
    }
}
