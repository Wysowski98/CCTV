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
    /// Logika interakcji dla klasy Page1.xaml
    /// </summary>
    public partial class Page1 : UserControl
    {
        public class ACCOUNT
        {
            public int Username { get; set; }
            public string Email { get; set; }
            public bool Role { get; set; }
            public bool Trans { get; set; }
        }
        public Page1()
        {
            InitializeComponent();
            role1.Text = "Admin";
            email1.Text = "lalalalal@o2.pl";
            username1.Text = "radpla";
            id1.Text = "123.567.456";


        }
    }
}