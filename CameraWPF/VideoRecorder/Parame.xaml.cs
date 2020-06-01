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


namespace CCTVSystem
{
    public partial class Parame : UserControl
    {
        public class Param
        {
            public String Quality { get; set; }
            public String Size { get; set; }
            public String Length { get; set; }

        }

        public Parame()
        {
            InitializeComponent();
            Combobox1.Items.Add("360");
            Combobox1.Items.Add("240");
            Combobox1.Items.Add("144");

            Combobox2.Items.Add("big");
            Combobox2.Items.Add("small");
            Combobox2.Items.Add("normal");


            Combobox3.Items.Add("140");
            Combobox3.Items.Add("67");
            Combobox3.Items.Add("70");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var values = new Param
            {
                Quality = Combobox1.SelectedItem.ToString(),
                 Size = Combobox2.SelectedItem.ToString(),
                 Length = Combobox3.SelectedItem.ToString()

             };
            MessageBox.Show("Saved");
        }

        private void Combobox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textbox1.Text = Combobox1.SelectedItem.ToString();
        }
        private void Combobox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textbox2.Text = Combobox2.SelectedItem.ToString();
        }
        private void Combobox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textbox3.Text = Combobox3.SelectedItem.ToString();
        }


    }
}
