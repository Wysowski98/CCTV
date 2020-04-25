using System;
using System.Collections.Generic;
using System.Linq;
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
using CCTVSystem.Client.ViewModels;

namespace CCTVSystem.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static HttpClient client = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
        }

        static async Task<List<ClientViewModel>> GetAPIAsync(string path)
        {

            List<ClientViewModel> clients = null;

            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)

            {

                clients = await response.Content.ReadAsAsync<List<ClientViewModel>>();

            }

            return clients;

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            List<ClientViewModel> clients = await GetAPIAsync("https://localhost:44309/api/Client");
            this.dataGrid1.ItemsSource = clients;
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            Main.Content = new AddClient();
        }
    }
}
