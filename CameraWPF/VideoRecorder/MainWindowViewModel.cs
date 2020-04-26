using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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

        private List<Camera> _cameras = new List<Camera>();

        #endregion

        #region Constructor

        public MainWindowViewModel()
        {
            for (int i = 0; i < 2; i++)
            {
                Cameras.Add(new Camera());
            }
        
            EnterIPCommand = MyCommand;
            StartRecordingCommand = new RelayCommand(startRecording);
            StopRecordingCommand = new RelayCommand(stopRecording);
            StopCameraCommand = new RelayCommand(stopCamera);  
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
            for (int i = 0; i < 2; i++)
            {
                Cameras[i].StopCamera();
            }
        }

        private void startRecording()
        {
            for (int i = 0; i < 2; i++)
            {
                Cameras[i].StartRecording();
            }
        }

        private void stopRecording()
        {
            for (int i = 0; i < 2; i++)
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
                switch(parameter.ToString())
                {
                    case "1":
                        {
                            Cameras[0].CameraUrl = newIp;
                            Cameras[0].StartCamera();
                            break;
                        }
                    case "2":
                        {
                            Cameras[1].CameraUrl = newIp;
                            Cameras[1].StartCamera();
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("Something wrong with passing button parameter!");
                            break;
                        }
                }
            }
        }

        private bool CanCommandExecute(object parameter)
        {
            return true;
        }
      
    }   
}
