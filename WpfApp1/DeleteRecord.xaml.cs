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
    public partial class DeleteRecords : UserControl
    {
        public class Recorded
        {
            public bool IsSelected { get; set; }
            public string FileName { get; set; }
            public string Data { get; set; }
            public long Length { get; set; }
            public int Id { get; set; }
        }

        public DeleteRecords()
        {
            InitializeComponent();
            List<Recorded> items = new List<Recorded>();
            items.Add(new Recorded() { IsSelected = true, FileName = "ab.mpeg", Id = 12, Data = "04.05.2020", Length = 81 });
            items.Add(new Recorded() { IsSelected = true, FileName = "bc.mpeg", Id = 3, Data = "03.05.2020", Length = 44 });
            items.Add(new Recorded() { IsSelected = false, FileName = "cd.mpeg", Id = 5, Data = "06.05.2020", Length = 125 });
            RecordHistory.ItemsSource = items;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
