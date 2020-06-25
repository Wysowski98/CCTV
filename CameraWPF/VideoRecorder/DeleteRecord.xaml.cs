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
            public string RecordingDate { get; set; }
            public int Hours { get; set; }
            public int Minutes { get; set; }
            public string Camera { get; set; }
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

            foreach(GetTransCommand gtc in _trans)
            {
                Recorded r = new Recorded();
                r.IsChecked = false;
                r.FileName = gtc.Filename;
                r.Hours = gtc.Hours;
                r.Minutes = gtc.Minutes;
                r.RecordingDate = gtc.RecordingDate.ToString();
                //tu trzeba poprawić/zmienić
                r.Camera = gtc.CameraId.ToString();
                RecordHistory.Items.Add(r);
            }        
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach(Recorded r in RecordHistory.Items)
            {
                if(r.IsChecked == true)
                {
                    MessageBox.Show("No tak");
                }
            }
        }
    }
}
