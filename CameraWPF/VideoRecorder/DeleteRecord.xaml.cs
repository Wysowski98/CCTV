using CCTVSystem.Client.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
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
        private List<Recorded> _dunno;

        public class Recorded
        {
            public int Id { get; set; }
            public bool? IsChecked { get; set; }
            public string FileName { get; set; }
            public string RecordingDate { get; set; }
            public int Hours { get; set; }
            public int Minutes { get; set; }
            public string Camera { get; set; }

            public bool ReadyToDelete { get; set; }

        }

        public DeleteRecords()
        {
            InitializeComponent();
            _dunno = new List<Recorded>();
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
                //RecordHistory.Items.Clear();
                foreach (GetTransCommand gtc in _trans)
                {
                    Recorded r = new Recorded();
                    r.Id = gtc.Id;
                    r.IsChecked = false;
                    r.FileName = gtc.Filename;
                    r.Hours = gtc.Hours;
                    r.Minutes = gtc.Minutes;
                    r.RecordingDate = gtc.RecordingDate.ToString();
                    r.Camera = gtc.CameraId.ToString();
                    r.ReadyToDelete = gtc.ReadyToDelete;
                    if(r.ReadyToDelete == false)
                    {
                        RecordHistory.Items.Add(r);
                        _dunno.Add(r);
                    }
             
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Recorded> objToDelete = new List<Recorded>();
            foreach (Recorded r in RecordHistory.Items)
            {
                if (r.IsChecked == true)
                {
                    deleteTrans(r.Id);
                    objToDelete.Add(r);                    
                }
            }

            if (!objToDelete.Any())
            {
                MessageBox.Show("There is nothing to delete");
            }
            else
            {
                foreach (Recorded r in objToDelete)
                {
                    RecordHistory.Items.Remove(r);
                }
            }

        }

        private async void deleteTrans(int idTransmission)
        {
            var response = await client.DeleteAsync("https://localhost:44309/admin/" + idTransmission);
            DeleteRecording(await response.Content.ReadAsStringAsync());
        }

        private async void DeleteRecording(string filename)
        {
            string[] filePaths = Directory.GetFiles(@"./", "*.avi",
                                         SearchOption.AllDirectories);
            
            try
            {
                if(filePaths != null)
                {
                    File.Delete(filePaths.First(x => x.Equals("./" + filename)));
                    Console.WriteLine("File deleted.");
                }
                    
            }
            catch (Exception ioExp)
            {
                Console.WriteLine(ioExp.Message);
            }
        }

        public void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            while (!worker.CancellationPending)
            {               
                TimeSpan timeToNextDelete = TimeSpan.FromMinutes(1);
                if (RecordHistory.Items.Count > 0)
                {
                    foreach (Recorded item in RecordHistory.Items)
                    {
                        if ((DateTime.Now - DateTime.Parse(item.RecordingDate)) > timeToNextDelete && !item.ReadyToDelete)
                        {                            
                            deleteTrans(item.Id);                            
                        }
                    }
                    break;
                }
            }
            
        }

    }


}

