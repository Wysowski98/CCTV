using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
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

namespace VideoRecorder
{
    class Camera : ObservableObject, IDisposable
    {
        #region Constructor
        public Camera()
        {
            cameraAmount++;
            cameraId = cameraAmount - 1;
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

        public void StartCamera()
        {
            VideoSource = new MJPEGStream(CameraUrl);
            VideoSource.NewFrame += video_NewFrame;
            VideoSource.Start();
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (recording)
            {
                using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
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
            using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
            {
                var bi = bitmap.ToBitmapImage();
                bi.Freeze();
                Dispatcher.CurrentDispatcher.Invoke(() => Image = bi);
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
                string filename = "vid";
                filename += cameraId + ".avi";
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

        private const int _maxCameras = 25;

        private List<Camera> _cameras = new List<Camera>();
        private WrapPanel _panelImages;
        private int _viewType = 9;

        #endregion

        #region Constructor

        public MainWindowViewModel(WrapPanel panelImages)
        {
            for (int i = 0; i < _maxCameras; i++)
            {
                Cameras.Add(new Camera());
            }
        
            EnterIPCommand = MyCommand;
            StartRecordingCommand = new RelayCommand(startRecording);
            StopRecordingCommand = new RelayCommand(stopRecording);
            StopCameraCommand = new RelayCommand(stopCamera);
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

        private void stopCamera()
        {
            for (int i = 0; i < _viewType; i++)
            {
                Cameras[i].StopCamera();
            }
        }

        private void startRecording()
        {
            for (int i = 0; i < _viewType; i++)
            {
                Cameras[i].StartRecording();
            }
        }

        private void stopRecording()
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

        private void CommandExecute(object parameter)
        {
            string newIp = new InputBox("New camera stream IP").ShowDialog();
            if (newIp == "CANCEL")
            {
                MessageBox.Show("Entering IP canceled");
            }
            else
            {
                int x = Int32.Parse(parameter.ToString());
                Cameras[x-1].CameraUrl = newIp;
                Cameras[x-1].StartCamera();              
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
