using System;
using System.Collections.Generic;
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

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy Transmission.xaml
    /// </summary>
    public partial class Transmission : UserControl
    {
        public class Values
        {
            public string W { get; set; }
            public string H { get; set; }
         
        }

        public Transmission()
        {
    
            InitializeComponent();
          
        }

     

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var values = new Values
            {
                W = this.textbox1.Text,
                H = this.textbox2.Text
                
             };
            

            MessageBox.Show("You Entered: \n" + "Width: " + textbox1.Text + "\nHeight: " + textbox2.Text);
           

        }

    
        private void KeyDown_textbox2(object sender, KeyEventArgs e)
        {
          
            var values = new Values
            {
                W = this.textbox1.Text,
                H = this.textbox2.Text

            };

            if (e.Key == Key.Return)
                MessageBox.Show("You Entered: \n" + "Width: " + textbox1.Text + "\nHeight: " + textbox2.Text);
            if (e.Key == Key.A || e.Key == Key.B || e.Key == Key.C || e.Key == Key.D || e.Key == Key.E || e.Key == Key.F || e.Key == Key.G || e.Key == Key.H || e.Key == Key.I || e.Key == Key.J || e.Key == Key.K || e.Key == Key.L || e.Key == Key.M || e.Key == Key.N || e.Key == Key.O || e.Key == Key.P || e.Key == Key.R || e.Key == Key.S || e.Key == Key.T || e.Key == Key.U || e.Key == Key.V || e.Key == Key.W || e.Key == Key.X || e.Key == Key.Y || e.Key == Key.Z)
            {
              
                MessageBox.Show("Enter correct value!");
                textbox2.Clear();
            }
        }

        private void KeyDown_textbox1(object sender, KeyEventArgs e)
        {

          
            if (e.Key == Key.A || e.Key == Key.B || e.Key == Key.C || e.Key == Key.D || e.Key == Key.E || e.Key == Key.F || e.Key == Key.G || e.Key == Key.H || e.Key == Key.I || e.Key == Key.J || e.Key == Key.K || e.Key == Key.L || e.Key == Key.M || e.Key == Key.N || e.Key == Key.O || e.Key == Key.P || e.Key == Key.R || e.Key == Key.S || e.Key == Key.T || e.Key == Key.U || e.Key == Key.V || e.Key == Key.W || e.Key == Key.X || e.Key == Key.Y || e.Key == Key.Z)
            {

                MessageBox.Show("Enter correct value!");
                textbox1.Clear();
            }
        }
    }
}
