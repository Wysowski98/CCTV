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


namespace CCTVSystem.Client
{
    public partial class History : UserControl
    {
        public class watchingHistory
        {
            public int Id { get; set; }
            public string Data { get; set; }
            public bool Recorded { get; set; }
        }

        public History()
        {
            InitializeComponent();
            List<watchingHistory> items = new List<watchingHistory>();
            items.Add(new watchingHistory() { Id = 12, Data = "04.05.2020", Recorded = true });
            items.Add(new watchingHistory() { Id = 3, Data = "03.05.2020", Recorded = false });
            items.Add(new watchingHistory() { Id = 5, Data = "06.05.2020", Recorded = false });
            Watched.ItemsSource = items;
        }
    }
}
