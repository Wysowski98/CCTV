using System;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Accord.Video.FFMPEG;
using AForge.Video;
using GalaSoft.MvvmLight;


namespace CCTVSystem.NetCamera
{
    class NetCamera : ObservableObject, IDisposable
    {
        #region Constructor
        public NetCamera()
        {
            cameraAmount++;
            cameraId = cameraAmount - 1;
        }

        public NetCamera(string url)
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
}
