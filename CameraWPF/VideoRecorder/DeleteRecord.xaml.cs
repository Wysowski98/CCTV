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

            if (_trans != null)
            {
                for (int i = 0; i < _trans.Count; i++)
                {                   
                    RecordHistory.Items.Add(_trans[i]);
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach(CheckBox cb in RecordHistory.Items)
            {
                if(cb.IsChecked == true)
                {
                    MessageBox.Show("No tak");
                }
            }
        }
    }
}
