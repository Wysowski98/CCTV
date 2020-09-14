using CCTVSystem.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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


namespace CCTVSystem.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker worker = new BackgroundWorker();
        private MainWindowViewModel mv;
        private ClientViewModel _loggedUser;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(ClientViewModel loggedClient)
        {
            InitializeComponent();
            singleImage.Visibility = System.Windows.Visibility.Hidden;
            mv = new MainWindowViewModel(singleImage, panelImages, loggedClient);
            this.DataContext = mv;
            _loggedUser = loggedClient;
            worker.WorkerSupportsCancellation = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
          
            GridCursor.Margin = new Thickness(0, (80 * index) , 0, 0 );
            UserControl usc = null;
            Cotu.Children.Clear();
            panelImages.Visibility = System.Windows.Visibility.Hidden;
            singleImage.Visibility = System.Windows.Visibility.Hidden;
            switch (index)
            {
                case 0:
                    panelImages.Visibility = System.Windows.Visibility.Visible;
                    singleImage.Visibility = System.Windows.Visibility.Hidden;
                    break;

                case 1:
                    usc = new DeleteUsers();
                    Cotu.Children.Add(usc);
                    break;

                case 2:
                    usc = new History();
                    Cotu.Children.Add(usc);
                    break;

                case 3:
                    if (worker.IsBusy == true)
                        worker.CancelAsync();
                    usc = new DeleteRecords();
                    if (worker.IsBusy != true)
                    {
                        worker.DoWork += new DoWorkEventHandler(((DeleteRecords)usc).Worker_DoWork);
                        worker.RunWorkerAsync();
                    }
                    Cotu.Children.Add(usc);
                    break;

                case 4:
                    usc = new password(_loggedUser);
                    Cotu.Children.Add(usc);
                    break;

               case 5:
                    usc = new InfoPage(_loggedUser);
                    Cotu.Children.Add(usc);
                    break;
            }
        }

        private async void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            await mv.getClientCameras();
        }

        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {

            Cotu.Children.Clear();
            panelImages.Visibility = System.Windows.Visibility.Visible;
            singleImage.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Close();
        }
    }
}
