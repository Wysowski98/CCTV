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

namespace CCTVSystem
{
    /// <summary>
    /// Logika interakcji dla klasy DeleteUsers.xaml
    /// </summary>
    public partial class DeleteUsers : UserControl
    {
        public class Users
        {
            public bool IsSelected { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
        }

        public DeleteUsers()
        {
            InitializeComponent();
            List<Users> items = new List<Users>();
            items.Add(new Users() { IsSelected = true, UserName = "rad", Email = "hyuj@op.pl", Role="User"});
            items.Add(new Users() { IsSelected = true, UserName = "an", Email = "edfwe@op.pl", Role = "User" });
            items.Add(new Users() { IsSelected = false, UserName = "dom", Email = "ede@o2.pl", Role = "User" });
            items.Add(new Users() { IsSelected = true, UserName = "dor", Email = "ds@gmail.com", Role = "User" });
            items.Add(new Users() { IsSelected = false, UserName = "da", Email = "cfdsad@dom.pl", Role = "User" });
            items.Add(new Users() { IsSelected = true, UserName = "ma", Email = "edsde@o2.pl", Role = "User" });
            items.Add(new Users() { IsSelected = false, UserName = "ds", Email = "edewde@o2.pl", Role = "User" });
            items.Add(new Users() { IsSelected = true, UserName = "sda", Email = "ededfds@o2.pl", Role = "User" });
            items.Add(new Users() { IsSelected = false, UserName = "sdsafdferferferferfrefddsafdssfdfsddfs", Email = "eaaade@o2.pl", Role = "User" });
            items.Add(new Users() { IsSelected = true, UserName = "dfsf", Email = "efdsfsdde@o2.pl", Role = "User" });
            items.Add(new Users() { IsSelected = false, UserName = "dfsf", Email = "de@o2.pl", Role = "User" });




            UsersList.ItemsSource = items;
        }
 
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
