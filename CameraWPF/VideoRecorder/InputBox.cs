using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VideoRecorder
{
    internal class InputBox
    {
        Window Box = new Window();//window for the inputbox
        int FontSize = 18;//fontsize for the input
        StackPanel sp1 = new StackPanel();// items container
        string title = "InputBox";//title as heading
        string boxcontent;//title
        string defaulttext = "Enter stream IP address here...";//default textbox content       
        string okbuttontext = "Turn on camera";
        string cancelbuttontext = "Cancel";
        string camerabuttontext = "Turn off the camera";
        string offrecordingfbuttontext = "Turn off the recording";
        string onrecordingbuttontext = "Turn on the recording";

        bool clicked = false;
        TextBox input = new TextBox();
        Button ok = new Button();
        Button cancel = new Button();
        Button camera = new Button();
        Button offrec = new Button();
        Button onrec = new Button();
        bool inputreset = false;

        public InputBox(string content)
        {
            try
            {
                boxcontent = content;
            }
            catch { boxcontent = "Error!"; }
            windowdef();
        }

        public InputBox(string content, string Htitle, string DefaultText)
        {
            try
            {
                boxcontent = content;
            }
            catch { boxcontent = "Error!"; }
            try
            {
                title = Htitle;
            }
            catch
            {
                title = "Error!";
            }
            try
            {
                defaulttext = DefaultText;
            }
            catch
            {
                DefaultText = "Error!";
            }
            windowdef();
        }

        public InputBox(string content, string Htitle, string Font, int Fontsize)
        {
            try
            {
                boxcontent = content;
            }
            catch { boxcontent = "Error!"; }
            try
            {
                title = Htitle;
            }
            catch
            {
                title = "Error!";
            }
            if (Fontsize >= 1)
                FontSize = Fontsize;
            windowdef();
        }

        private void windowdef()// window building - check only for window size
        {
            Box.Height = 500;// Box Height
            Box.Width = 300;// Box Width
            Box.Title = title;
            Box.Content = sp1;
            Box.Closing += Box_Closing;
            TextBlock content = new TextBlock();
            content.TextWrapping = TextWrapping.Wrap;
            content.Background = null;
            content.HorizontalAlignment = HorizontalAlignment.Center;
            content.Text = boxcontent;
            content.FontSize = FontSize;
            sp1.Children.Add(content);

            input.FontSize = FontSize;
            input.HorizontalAlignment = HorizontalAlignment.Center;
            input.Text = defaulttext;
            input.MinWidth = 200;
            input.MouseEnter += input_MouseDown;
            sp1.Children.Add(input);

            ok.Width = 140;
            ok.Height = 30;
            ok.Click += ok_Click;
            ok.Content = okbuttontext;
            ok.HorizontalAlignment = HorizontalAlignment.Center;
            sp1.Children.Add(ok);

            camera.Width = 140;
            camera.Height = 30;
            camera.Click += cameraoff_Click;
            camera.Content = camerabuttontext;
            camera.HorizontalAlignment = HorizontalAlignment.Center;
            sp1.Children.Add(camera);

            onrec.Width = 140;
            onrec.Height = 30;
            onrec.Click += onrec_Click;
            onrec.Content = onrecordingbuttontext;
            onrec.HorizontalAlignment = HorizontalAlignment.Center;
            sp1.Children.Add(onrec);

            offrec.Width = 140;
            offrec.Height = 30;
            offrec.Click += offrec_Click;
            offrec.Content = offrecordingfbuttontext;
            offrec.HorizontalAlignment = HorizontalAlignment.Center;
            sp1.Children.Add(offrec);

            cancel.Width = 70;
            cancel.Height = 30;
            cancel.Click += cancel_Click;
            cancel.Content = cancelbuttontext;
            cancel.HorizontalAlignment = HorizontalAlignment.Center;
            sp1.Children.Add(cancel);

        }

        void Box_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!clicked)
                e.Cancel = true;
        }

        private void input_MouseDown(object sender, MouseEventArgs e)
        {
            if ((sender as TextBox).Text == defaulttext && inputreset == false)
            {
                (sender as TextBox).Text = null;
                inputreset = true;
            }
        }

        void ok_Click(object sender, RoutedEventArgs e)
        {
            clicked = true;
            Box.Close();
        }

        void cancel_Click(object sender, RoutedEventArgs e)
        {
            clicked = true;
            Box.Close();
            input.Text = "CANCEL";
        }

        void onrec_Click(object sender, RoutedEventArgs e)
        {
            clicked = true;
            Box.Close();
            input.Text = "REC_ON";
        }

        void offrec_Click(object sender, RoutedEventArgs e)
        {
            clicked = true;
            Box.Close();
            input.Text = "REC_OFF";
        }

        void cameraoff_Click(object sender, RoutedEventArgs e)
        {
            clicked = true;
            Box.Close();
            input.Text = "CAM_OFF";
        }

        public string ShowDialog()
        {
            Box.ShowDialog();
            return input.Text;
        }
    }
}