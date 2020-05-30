using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
          
            GridCursor.Margin = new Thickness(0, (80 * index) , 0, 0 );
            UserControl usc = null;
            Cotu.Children.Clear();
            switch (index)
            {
           
                case 1:
                    usc = new Transmission();
                    Cotu.Children.Add(usc);
                    break;

                case 2:
                    usc = new History();
                    Cotu.Children.Add(usc);
                    break;

                case 3:
                    usc = new DeleteRecords();
                    Cotu.Children.Add(usc);
                    break;

                case 4:
                    usc = new password();
                    Cotu.Children.Add(usc);
                    break;

               case 5:
                    usc = new InfoPage();
                    Cotu.Children.Add(usc);
                    break;
            }
        }
    }
}
