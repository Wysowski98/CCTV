using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy password.xaml
    /// </summary>
    public partial class password : UserControl
    {
        public class ACCOUNT
        {
            public int oldp { get; set; }
            public string new2p { get; set; }
            public bool new3p { get; set; }
        }
        public System.Security.SecureString SecurePassword { get; }


        public password()
        {
            InitializeComponent();

        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {


            //polaczyc z tym od dawida zeby zmienic haslo z obecnego juz z  ClientViewModel
            if (new2.Password != new3.Password)
                    MessageBox.Show("Hasła nie są identyczne.");
                else if (old.Password == new3.Password)
                    MessageBox.Show("Nowe hasło nie różni się od starego");

                else if (old.Password == new2.Password)
                    MessageBox.Show("Nowe hasło nie różni się od starego");
                else if (new3.SecurePassword.Length < 5)
                    MessageBox.Show("Za krotkie hasło");
                else
                {
                    MessageBox.Show("Zmieniono hasło");

                }

        }

    }
}
