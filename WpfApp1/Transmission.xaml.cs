﻿using System;
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
        public int[] count { get; set; }
        public Transmission()
        {
    
            InitializeComponent();
            for (int i = 1; i < 50; i++)
            {
                ComboBox1.Items.Add(i);
                ComboBox2.Items.Add(i);
            }
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
