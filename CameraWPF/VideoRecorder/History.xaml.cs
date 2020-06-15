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
using System.Windows.Shapes;
using VideoRecorder.VMs;

namespace CCTVSystem.Client
{
    public partial class History : UserControl
    {
        static HttpClient client = new HttpClient();
        private List<HistoryCommand> _trans;

        private class watchingHistory
        {
            public Camera Camera { get; set; }

            public DateTime RecordingDate { get; set; }

            public bool IsRecording { get; set; }
        }

        public History()
        {
            InitializeComponent();
            getTransHistory();
        }

        public async void getTransHistory()
        {
            var response = await client.GetAsync("https://localhost:44309/api/Trans/getTranss");
            //jezeli serwer wyslal pozytywna odpowiedz
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string resp = await response.Content.ReadAsStringAsync();
                _trans = JsonConvert.DeserializeObject<List<HistoryCommand>>(resp);

                for (int i = 0; i < _trans.Count; i++)
                {
                    Watched.Items.Add(_trans[i]);
                }

            }

        }
    }
}