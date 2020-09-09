using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Accord.Video.FFMPEG;
using AForge.Video;
using AForge.Video.DirectShow;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace CCTVSystem.Client.ViewModels
{
    class Camera : ObservableObject, IDisposable
    {
        #region Constructor
        public Camera()
        {
            cameraAmount++;
            cameraId = cameraAmount - 1;
        }

        public Camera(string url)
        {
            cameraAmount++;
            cameraId = cameraAmount - 1;
            cameraUrl = url;
            StartCamera();
        }
        #endregion

        #region Private fields

        private static int cameraAmount = 0;
        private int cameraId;
        private BitmapImage image;
        private VideoFileWriter writer;
        private bool recording;
        private DateTime? firstFrameTime;
        private IVideoSource videoSource;
        private string cameraUrl;
        private string filename;

        #endregion

        #region Properties

        public BitmapImage Image
        {
            get { return image; }
            set { Set(ref image, value); }
        }

        private IVideoSource VideoSource
        {
            get { return videoSource; }
            set { Set(ref videoSource, value); }
        }

        #endregion

        public string CameraUrl
        {
            get { return cameraUrl; }
            set { Set(ref cameraUrl, value); }
        }

        public string Filename
        {
            get { return filename; }
            set { Set(ref filename, value); }
        }

        public void StartCamera()
        {
            VideoSource = new MJPEGStream(CameraUrl);
            VideoSource.NewFrame += video_NewFrame;
            VideoSource.Start();
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
                using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
                {
                    var bi = bitmap.ToBitmapImage();
                    bi.Freeze();
                    Dispatcher.CurrentDispatcher.Invoke(() => Image = bi);

                    if (recording)
                    {
                        if (firstFrameTime != null)
                        {
                            writer.WriteVideoFrame(bitmap, DateTime.Now - firstFrameTime.Value);
                        }
                        else
                        {
                            writer.WriteVideoFrame(bitmap);
                            firstFrameTime = DateTime.Now;
                        }
                    }
                }          

        }

        public void StopCamera()
        {
            if (VideoSource != null && VideoSource.IsRunning)
            {
                VideoSource.SignalToStop();
                VideoSource.NewFrame -= video_NewFrame;
            }
            Image = null;           
        }

        public void StopRecording()
        {
            if (writer != null)
            {
                recording = false;
                writer.Close();
                writer.Dispose();
            }
        }

        public void StartRecording()
        {
            if (Image != null)
            {
                filename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                filename += ".avi";
                firstFrameTime = null;
                writer = new VideoFileWriter();
                writer.Open(filename, (int)Math.Round(Image.Width, 0), (int)Math.Round(Image.Height, 0));
                recording = true;
            }
        }

        public void Dispose()
        {
            if (VideoSource != null && VideoSource.IsRunning)
            {
                VideoSource.SignalToStop();
            }
            writer?.Dispose();
        }
    }

    class MainWindowViewModel : ObservableObject
    {
        #region Private fields

        private ClientViewModel _loggedClient;
        private static HttpClient client = new HttpClient();

        private const int _maxCameras = 15;

        private List<CameraViewModel> _clientCameras = new List<CameraViewModel>();
        private List<Camera> _cameras = new List<Camera>();
        private WrapPanel _panelImages;
        private System.Windows.Controls.Image _singleImage;
        private int _viewType = 15;

        #endregion

        #region Constructor

        public MainWindowViewModel(System.Windows.Controls.Image singleImage, WrapPanel panelImages, ClientViewModel loggedClient)
        {
            _loggedClient = loggedClient;
            _singleImage = singleImage;

            for (int i = 0; i < _maxCameras; i++)
            {           
                Cameras.Add(new Camera());
            }
         
            EnterIPCommand = MyCommand;
            StartRecordingCommand = new RelayCommand(startAllRecordings);
            StopRecordingCommand = new RelayCommand(stopAllRecordings);
            StopCameraCommand = new RelayCommand(stopAllCameras);
            _panelImages = panelImages;
            prepButtons(_viewType);          
        }

        #endregion

        #region Properties

        public List<Camera> Cameras
        {
            get
            {
                return _cameras;
            }
            set { Set(ref _cameras, value); }
        }

        public ICommand EnterIPCommand { get; private set; }

        public ICommand StartRecordingCommand { get; private set; }

        public ICommand StopRecordingCommand { get; private set; }

        public ICommand StopCameraCommand { get; private set; }

        #endregion

        public async Task getClientCameras()
        {
            var myContent = JsonConvert.SerializeObject(_loggedClient);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await client.PostAsync("https://localhost:44309/api/Camera/GetCams", byteContent);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var _cams = JsonConvert.DeserializeObject<List<CameraViewModel>>(responseBody);
                int i = 0;
                foreach (var cam in _cams)
                {
                    if (i > 14)
                        break;
                    Cameras[i].CameraUrl = cam.IpAddress;
                    Cameras[i].StartCamera();
                    i++;
                }
            }
            else
                MessageBox.Show("Bład uzyskiwania kamer użytkownika!");
        }

        private void stopAllCameras()
        {
            for (int i = 0; i < _viewType; i++)
            {
                Cameras[i].StopCamera();
            }
        }

        private void startAllRecordings()
        {
            for (int i = 0; i < _viewType; i++)
            {
                Cameras[i].StartRecording();
            }
        }

        private void stopAllRecordings()
        {
            for (int i = 0; i < _viewType; i++)
            {
                Cameras[i].StopRecording();
            }
        }

        public ICommand MyCommand

        {
            get
            {
                if (EnterIPCommand == null)
                {
                    EnterIPCommand = new RelayCommand<object>(CommandExecute, CanCommandExecute);
                }
                return EnterIPCommand;
            }

        }

        private async void CommandExecute(object parameter)
        {
            string newIp = new InputBox("Panel kamery").ShowDialog();
            int i = Int32.Parse(parameter.ToString());
            i--;
          
            switch (newIp)
            {
                case "CANCEL":
                    {
                        MessageBox.Show("Anulowano akcję na kamerze");
                        break;
                    }
                case "CAM_OFF":
                    {
                        Cameras[i].StopCamera();
                        break;
                    }
                case "REC_ON":
                    {
                        Cameras[i].StartRecording();
                        MessageBox.Show("Rozpoczęto nagrywanie na kamerze " + (i+1) + "!");

                        //Adding transmission
                        var values = new CameraCommand
                        {
                            Url = Cameras[i].CameraUrl,
                            ClientId = _loggedClient.Id
                        };
                        var values2 = new TransmissionCommand
                        {
                            RecordingDate = DateTime.Now,
                            CamRequest = values,
                            FileName = Cameras[i].Filename
                        };

                        var myContent = JsonConvert.SerializeObject(values2);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        var response = await client.PostAsync("https://localhost:44309/api/Trans/addTranss", byteContent);

                        if (response.StatusCode != HttpStatusCode.OK)
                            MessageBox.Show("Bład rejestrowania transmisji kamery!");
                        break;
                    }
                case "REC_OFF":
                    {
                        Cameras[i].StopRecording();
                        MessageBox.Show("Zatrzymano nagrywanie na kamerze " + (i+1) + "!");
                        break;
                    }
                case "SHOW_SINGLE":
                    {
                        Binding myBinding = new Binding("Cameras[" + i + "].Image");
                        _singleImage.Visibility = System.Windows.Visibility.Visible;
                        _singleImage.SetBinding(System.Windows.Controls.Image.SourceProperty, myBinding);
                        _panelImages.Visibility = System.Windows.Visibility.Hidden;
                        break;
                    }
                default:
                    {
                        if (i < 0 || i > 24)
                        {
                            MessageBox.Show("Bład przekazywania parametru przycisku!");
                        }
                        else
                        {
                            Cameras[i].CameraUrl = newIp;

                            //Adding camera
                            var values = new CameraCommand
                            {
                                Url = newIp,
                                ClientId = _loggedClient.Id
                            };
                            var myContent = JsonConvert.SerializeObject(values);
                            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                            var byteContent = new ByteArrayContent(buffer);
                            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            HttpResponseMessage response = await client.PostAsync("https://localhost:44309/api/Camera/AddCam", byteContent);

                            if (response.StatusCode != HttpStatusCode.OK)
                                MessageBox.Show("Bład dodawania nowej kamery!");

                            Cameras[i].StartCamera();                           
                        }
                        break;
                    }
            }
        }

        private bool CanCommandExecute(object parameter)
        {
            return true;
        }

        private void prepButtons(int x)
        {
            _panelImages.Children.Clear();
            for (int i = 0; i < x; i++)
            {
                System.Windows.Controls.Image img = new System.Windows.Controls.Image();
                Button b = new Button();

                b.Command = EnterIPCommand;
                b.CommandParameter = i + 1;
                b.BorderThickness = new Thickness(1.0);
                b.Width = 100;
                b.Height = 100;

                Binding myBinding = new Binding("Cameras["+i+"].Image");              
                img.SetBinding(System.Windows.Controls.Image.SourceProperty, myBinding);

                b.Content = img;

                _panelImages.Children.Add(b);
            }
        }

    }   
}
