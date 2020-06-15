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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace CCTVSystem.Client
{
    public partial class DeleteRecords : UserControl
    {
        static HttpClient client = new HttpClient();
        private List<GetTransCommand> _trans;
        public class Recorded
        {
            public bool? IsChecked { get; set; }
            public string FileName { get; set; }
            public string Data { get; set; }
            public int Length { get; set; }
            public int Id { get; set; }
           
        }

        public DeleteRecords()
        {
            InitializeComponent();
            //   List<Recorded> items = new List<Recorded>();
            //   items.Add(new Recorded() { IsSelected = true, FileName = "ab.mpeg", Id = 12, Data = "04.05.2020", Length = 81 });
            //  items.Add(new Recorded() { IsSelected = true, FileName = "bc.mpeg", Id = 3, Data = "03.05.2020", Length = 44 });
            //   items.Add(new Recorded() { IsSelected = false, FileName = "cd.mpeg", Id = 5, Data = "06.05.2020", Length = 125 });
            //  items.Add(new Recorded() { IsSelected = false, FileName = "dsfsdfdsfds", Id = 5, Data = "06.05.2020", Length = 125 });
            //  RecordHistory.ItemsSource = items;
            getTrans();
        }


        private async void getTrans()
        {
            var response = await client.GetAsync("https://localhost:44309/api/Trans/getTranss");
            //jezeli serwer wyslal pozytywna odpowiedz
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string resp = await response.Content.ReadAsStringAsync();
                _trans = JsonConvert.DeserializeObject<List<GetTransCommand>>(resp);

            }


            for (int i = 0; i < _trans.Count; i++)
            {
                RecordHistory.Items.Add(_trans[i]);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
